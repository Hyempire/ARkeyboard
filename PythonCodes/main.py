from cvzone.HandTrackingModule import HandDetector
import cv2
import socket

cap = cv2.VideoCapture(0)   # 웹캠이면 0, USB캠이면 1
cap.set(3, 1280)
cap.set(4, 720)
success, img = cap.read()
h, w, _ = img.shape
detector = HandDetector(detectionCon=0.8, maxHands=2)

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 60006)
# local : 127.0.0.1
# Home wifi : 192.168.0.11 / 192.168.219.109
# Yonsei_Web : 172.24.207.234 / 172.24.207.152
# pi : 192.168.0.87

while True:
    # Get image frame
    success, img = cap.read()
    # Find the hand and its landmarks
    hands, img = detector.findHands(img)  # with draw
    # hands = detector.findHands(img, draw=False)  # without draw
    data1 = []
    data2 = []

    if hands:
        # Hand 1 : data1=['Left', '좌', '표', '들'] _ 또는 'Right'
        if len(hands) == 1:
            hand1 = hands[0]
            handType1 = hand1["type"]
            data1 = [handType1[0]]
            lmList1 = hand1["lmList"]  # List of 21 Landmark points
            for lm in lmList1:
                data1.extend([lm[0], h-lm[1], lm[2]])     # USB캠이면 w - l,[0]---------------
            # print(data1)
            sock.sendto(str.encode(str(data1)), serverAddressPort)

        # Hand 2 : data2=['Left', '좌', '표', '들', 'Right', '좌', '표', '들] _ Left Right 순서 다를 수도 있음
        elif len(hands) == 2:
            # Hand 1
            hand1 = hands[0]
            handType1 = hand1["type"]
            data1 = [handType1[0]]
            lmList1 = hand1["lmList"]  # List of 21 Landmark points
            for lm in lmList1:
                data1.extend([lm[0], h-lm[1], lm[2]])     # USB캠이면 w - l,[0]----------------
            # Hand2
            hand2 = hands[1]
            handType2 = hand2["type"]
            data2 = data1 + [handType2[0]]
            lmList2 = hand2["lmList"]
            for lm in lmList2:
                data2.extend([lm[0], h-lm[1], lm[2]])     # USB캠이면 w - l,[0]----------------
            # print(data2)
            sock.sendto(str.encode(str(data2)), serverAddressPort)

    img = cv2.resize(img, (0,0), None, 0.5, 0.5)
    # Display
    cv2.imshow("Image", cv2.flip(img,1))
    cv2.waitKey(1)

sock.close()
