# OpenCV, mediapipe
import cv2
import mediapipe as mp
import os.path

mp_drawing = mp.solutions.drawing_utils     # 얘는 무슨 뜻인지 모르겠다
mp_hands = mp.solutions.hands               # 손동작인식을 위한 모듈

cap = cv2.VideoCapture(0)                   # OpenCV로 웹캠을 읽어 입력데이터 소스로 지정한다. 웹캠이 두개 이상인 경우엔 0, 1, 2 등 여러 숫자로 지정

# with expressions as 객체 : 사용이 끝난 객체를 별도로 close하지 않아도 종료시킴
with mp_hands.Hands(
    max_num_hands=2,                        # 인식할 손모양의 갯수, default 2
    min_detection_confidence=0.5,           # 성공적인 것으로 간주되는 최소 신뢰도 값. 0.0~0.1 사이
    min_tracking_confidence=0.5) as hands:  # 손 랜드마크가 성공적으로 추척된 것으로 간주되는 최소 신뢰도 값. 0.0~0.1 사이
                                            # 값을 높이면 시간이 더 소요되지만 좀 더 정확한 작동 보장
    while cap.isOpened():
        success, image = cap.read()         # cap.read() : 재생되는 비디오를 한 프레임씩 읽음.
                                            # success : 프레임을 제대로 읽었는지, image : 읽은 프레임

        image_height, image_width, _ = image.shape

        if not success:
            continue
        image = cv2.cvtColor(cv2.flip(image, 1), cv2.COLOR_BGR2RGB)
        # OpenCV 영상은 BGR 형식인데, MediaPipe는 RGB를 사용하므로 영상형식을 변환한다
        # cv2.flip -> 1은 좌우 반전, 0은 상하 반전

        results = hands.process(image)      # 이 한 줄로서 손동작 인식 AI모델이 작동되고, 결과 값이 result에 저장됨

        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)      # RGB로 변환했던걸 다시 BGR로 변환

        if results.multi_hand_landmarks:                    # result값이 정상인 경우에만 후속 작업 처리
            for hand_landmarks in results.multi_hand_landmarks:
                finger1 = int(hand_landmarks.landmark[4].x * 100)       # result값으로 반환된 landmark(특징점) 데이터를 사용
                finger2 = int(hand_landmarks.landmark[8].x * 100)       # 4는 엄지손가락 끝, 8은 집게손가락 끝임(손가락index값 사진파일 참고)
                # landmark의 좌표값이 0.0~1.0사이의 값으로 반환됨 -> 100을 곱해서 100분율로 표시한 것.
                # 인덱스 뒤에 .x가 붙은걸 보면 여기선 x축 값을 저장하나 봄
                dist = abs(finger1 - finger2)       # abs: 절댓값 => 두 손가락 끝이 벌어진 정도를 계산
                cv2.putText(
                    image, text='f1=%d f2=%d dist=%d' % (finger1,finger2,dist), org=(10,30),    # finger1,2와 그 거리의 x축 위치값들을 출력
                    fontFace=cv2.FONT_HERSHEY_SIMPLEX, fontScale=1,                             # org: 출력 문자 시작 위치(좌측 하단 기준) 좌표,
                    color=255, thickness=3)

                mp_drawing.draw_landmarks(
                    image, hand_landmarks, mp_hands.HAND_CONNECTIONS)       # 랜드마크 그림 그려줌

                # 손가락 마디마디에 이미지 띄우기
                for i in range(0, len(hand_landmarks.landmark)):

                    keypad_address = f".\keypad\{i}.png"

                    if os.path.isfile(keypad_address):
                        keypad = cv2.imread(keypad_address, cv2.IMREAD_COLOR)

                        if (15 < hand_landmarks.landmark[i].x * image_width < 625) & (25 < hand_landmarks.landmark[i].y * image_height < 455):

                            # cv2.resize로 작게좀 만들기
                            keypad_img = cv2.resize(keypad, dsize=(30,30), interpolation=cv2.INTER_AREA)

                            # 키패드 크기 구하기
                            rows, cols, channels = keypad_img.shape

                            startX = int(hand_landmarks.landmark[i].x * image_width - 15)
                            startY = int(hand_landmarks.landmark[i].y * image_height - 5)
                            endX = int(hand_landmarks.landmark[i].x * image_width + 15)
                            endY = int(hand_landmarks.landmark[i].y * image_height + 25)

                            # roi (rigion of interest) 관심영역 지정
                            roi = image[startY:endY,startX:endX]

                            # 마스크, 역마스크 생성
                            imgGray = cv2.cvtColor(keypad_img, cv2.COLOR_BGRA2GRAY)
                            ret, mask = cv2.threshold(imgGray, 10, 255, cv2.THRESH_BINARY)
                            mask_inv = cv2.bitwise_not(mask)

                            # roi에서 mask_inv의 검정 부분 검정색으로 만들기
                            image_bg = cv2.bitwise_and(roi, roi, mask=mask_inv)

                            # keypad_img에서 색 있는 부분만 추출하기
                            img_fg = cv2.bitwise_and(keypad_img, keypad_img, mask=mask)

                            # 위에서 합성한 두 image_bg, img_fg 더하기
                            dst = cv2.add(image_bg, img_fg)
                            image[startY:endY,startX:endX] = dst

                        else:
                            # print("오류오류오류오류오류오류오류오류올오로오ㅓㅠ로율")
                            continue

                    else:
                        continue

        cv2.imshow('image', image)      # 아까 불러온 프레임들을 image라는 이미지 창 이름으로
        if cv2.waitKey(1) == 27:      # waitKey: 키 입력을 기다리는 대기함수: 매개변수 내 숫자의 시간(ms단위) 안에 키를 입력하면 다음 줄로 이동
                                            #           반대로, 제한 시간 내에 키를 입력하지 않으면 break되지 않겠네
                                            # 매개변수 0이면 무한대기 (키보드 입력이 있을 때까지 break 안됨)
                                            # ord('q') : q를 누르면 종료
            break

cap.release()
