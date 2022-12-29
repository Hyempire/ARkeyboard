using UnityEngine;
using TMPro;
using System;
using System.Text;

public class InputText : MonoBehaviour
{
    #region Private Variable
    private TMP_InputField textVariable;
    private int count = 0;
    private int recentKeyNumber;
    private string inputText;
    private int inputTextLength;
    private string inputString;
    private bool isgoing;
    public bool language;
    private string[] hangelKeys = { "ㅣ", "•", "ㅡ", "ㄱㅋㄲ", "ㄴㄹ", "ㄷㅌㄸ", "ㅂㅍㅃ", "ㅅㅎㅆ", "ㅈㅊㅉ", "ㅇㅁ" };
    private string[] hangelMoeumKeys = { "ㅣ", "•", "ㅡ" };
    private string[] hangelJaeumKeys = { "ㄱㅋㄲ", "ㄴㄹ", "ㄷㅌㄸ", "ㅂㅍㅃ", "ㅅㅎㅆ", "ㅈㅊㅉ", "ㅇㅁ" };
    private string[] englishKeys = { ".,?!", "abc", "def", "ghi", "jkl", "mno", "tuv", "wxyz", "pqrs", "space" };
    private string cho = "";
    private string jung = "";
    private string jong = "";
    private bool choPossible;
    private bool jungPossible;
    private bool jongPossible;
    private bool jongFirst = true;


    private bool hangelPossible;

    private string moeum1;
    private string moeum2;
    private string moeum;
    private string jaeum1;
    private string jaeum2;
    private string jaeum;

    private bool startMouemBuild;
    private bool startJonseongBuild;

    private string hangel;
    #endregion

    BackSpaceDetected backSpaceDetected;

    private void Start()
    {
        textVariable = GetComponent<TMP_InputField>();
        textVariable.text = "";
        language = true;
        startMouemBuild = true;
        startJonseongBuild = true;

        choPossible = true;
        jungPossible = false;
        jongPossible = false;
        hangelPossible = false;

        backSpaceDetected = GameObject.Find("Manager").GetComponent<BackSpaceDetected>();
    }

    

    public string jungBuilder(string moeum1, string moeum2)
    {
        // 중성 개수 21개 "ㅣ", "ㅡ" 제외 19개 케이스
        if (!choPossible)
        {
            jongPossible = true;
        }
        switch (moeum1, moeum2) {
            case ("ㅣ", "•"):
                return "ㅏ";
            case ("ㅏ","ㅣ"):
                return "ㅐ";
            case ("ㅏ", "•"):
                return "ㅑ";
            case ("ㅑ","ㅣ"):
                return "ㅒ";
            case ("•", "ㅣ"):
                return "ㅓ";
            case ("ㅓ","ㅣ"):
                return "ㅔ";
            case ("•", "•"): // 21개 포함 x
                if (!choPossible)
                {
                    jongPossible = false;
                }
                return "••";
            case ("••", "ㅣ"):
                return "ㅕ";
            case ("ㅕ","ㅣ"):
                return "ㅖ";
            case ("••", "ㅡ"):
                return "ㅛ";
            case ("•", "ㅡ"):
                return "ㅗ";
            case ("ㅗ","ㅣ"):
                return "ㅚ";
            case ("ㅚ", "•"):
                return "ㅘ";
            case ("ㅘ","ㅣ"):
                return "ㅙ";
            case ("ㅡ","ㅣ"):
                return "ㅢ";
            case ("ㅡ", "•"):
                return "ㅜ";
            case ("ㅜ", "•"):
                return "ㅠ";
            case ("ㅜ","ㅣ"):
                return "ㅟ";
            case ("ㅠ", "ㅣ"):
                return "ㅝ";
            case ("ㅝ","ㅣ"):
                return "ㅞ";
            default:
                jongPossible = false;
                return moeum1 + moeum2;
        }
    }

    public string jongBuilder(string jaeum1, string jaeum2)
    {
        // 중성 개수 21개 "ㅣ", "ㅡ" 제외 19개 케이스
        // jongPossible = false;
        // choPossible = true;
        print("종성 만들자!");
        print("자음1" + jaeum1 + "자음2" + jaeum2);
        switch (jaeum1, jaeum2)
        {
            case ("ㄱ", "ㅅ"):
                return "ㄳ";
            case ("ㄴ", "ㅈ"):
                return "ㄵ";
            case ("ㄴ", "ㅎ"):
                return "ㄶ";
            case ("ㄹ", "ㄱ"):
                return "ㄺ";
            case ("ㄹ", "ㅁ"):
                return "ㄻ";
            case ("ㄹ", "ㅂ"):
                return "ㄼ";
            case ("ㄹ", "ㅅ"): 
                return "ㄽ";
            case ("ㄹ", "ㅌ"):
                return "ㄾ";
            case ("ㄹ", "ㅍ"):
                return "ㄿ";
            case ("ㅂ", "ㅅ"):
                return "ㅄ";
            default:
                return jaeum1 + jaeum2;
        }
    }

    public void getInput(int keyNumber)
    {
        if (!backSpaceDetected.isBackspaceTriggered_)
        {
            isgoing = isContinuous(keyNumber);

            if (language)
            {
                inputText = hangelKeys[keyNumber];
            }
            else
            {
                inputText = englishKeys[keyNumber];
            }

            inputTextLength = inputText.Length;

            if (language)
            {
                hangelBuilder(inputText, inputTextLength, keyNumber);
            }
            else
            {
                englishBuilder(inputText, inputTextLength, keyNumber);
            }
        }
        else
        {
            Debug.Log("Backspace Ongoing");
        }
    }

    public void chojongjungBuilder(string cho, string jung, string jong)
    {
        textVariable = GetComponent<TMP_InputField>();
        string choseng = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        string jungseng = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
        string jonseng = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
        ushort first = 0xAC00;
        ushort last = 0xD79F;

        int choIndex, jungIndex, jongIndex;
        int uni;

        print("초성"+cho);
        print("중성"+jung);
        print("종성"+jong);

        if (cho != "")
        {
            char c = char.Parse(cho);
            choIndex = choseng.IndexOf(c);    // 초성 위치
        }
        else
        {
            choIndex = 0;
        }
        if (jung != "")
        {
            char ju = char.Parse(jung);
            jungIndex = jungseng.IndexOf(ju);   // 중성 위치
        }
        else
        {
            jungIndex = 0;
        }
        if (jong != "")
        {
            char jo = char.Parse(jong);
            jongIndex = jonseng.IndexOf(jo);   // 종성 위치
        }
        else
        {
            jongIndex = 0;
        }

      
        // 앞서 만들어 낸 계산식
        uni = first + (choIndex * 21 + jungIndex) * 28 + jongIndex;

        // 코드값을 문자로 변환
        char hangel = Convert.ToChar(uni);

        // "ㄱ" + "ㅏ" + "" => 가, "ㄱ" + "" + "" => 가

        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 1) + hangel;
        // return hangel;
    }

    public void hangelBuilder(string inputText, int inputTextLength, int keyNumber)
    {
        textVariable = GetComponent<TMP_InputField>();

        if (Array.IndexOf(hangelJaeumKeys, inputText) > -1 )
        {
            // 초성
            startMouemBuild = true;
            if (choPossible)
            {
                if (isgoing && inputTextLength == 2)
                {
                    count += 1;
                    count %= 2;
                    inputString = inputText.Substring(count, 1);
                    cho = inputString;
                    textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 1) + inputString;
                }

                else if (isgoing && inputTextLength == 3)
                {
                    count += 1;
                    count %= 3;
                    inputString = inputText.Substring(count, 1);
                    cho = inputString;
                    textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 1) + inputString;
                }

                else // ㄱ 다음에 ㄴ 들어온 경우 자음 다른키 입력;
                {
                    recentKeyNumber = keyNumber;
                    inputString = inputText.Substring(0, 1);
                    cho = inputString;
                    textVariable.text = textVariable.text + inputString;
                    count = 0;
                }

                // 초성 다음엔 중성이 들어올 수 있다. 

                jungPossible = true;

            }

            // 중성 다음 => 종성
            if (jongPossible)
            {
                print("여긴진짜 오지?");
                

                if (isgoing)
                {

                    if (inputTextLength == 2)
                    {
                        count += 1;
                        count %= 2;
                        jaeum1 = inputText.Substring(count, 1);
                        jaeum = jaeum1;
                        jong = jaeum;
                    }

                    else if (inputTextLength == 3)
                    {
                        count += 1;
                        count %= 3;
                        jaeum1 = inputText.Substring(count, 1);
                        jaeum = jaeum1;
                        jong = jaeum;
                    }
                }

                else // ㄱ 다음에 ㄴ 들어온 경우 자음 다른키 입력;;;
                {
                    recentKeyNumber = keyNumber;
                    jaeum1 = inputText.Substring(0, 1);
                    jaeum = jongBuilder(jaeum1, jaeum2);

                    if (jaeum.Length >= 2) // 예를 들면 만 + ㄷ
                    {
                        jaeum = jaeum2;
                        jaeum1 = "";
                        jaeum2 = "";
                        hangelPossible = false;
                        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length) + jaeum;
                        cho = jaeum;
                        jong = "";
                        jongPossible = false;
                        choPossible = true;
                        jung = "";
                    }

                    else // jongseong 만듦 예를 들면 듫
                    {
                        jong = jaeum;
                        hangelPossible = true;
                    }
                }
                
            }

            

        }

        // 모음 입력받는 경우 
        if (Array.IndexOf(hangelMoeumKeys, inputText) > -1) // ㅣ or ㆍ or ㅡ
        {
            count = 0;
            jongFirst = true;
            jaeum1 = "";
            jaeum2 = "";

            // 종성 다음 모음 국 + ㅣ => 국기
            if(jong != "")
            {
                if (inputText == "ㆍ")
                {
                    chojongjungBuilder(cho, jung, "");
                    textVariable.text = textVariable.text + jong + inputText;
                    cho = jong;
                    jong = "";
                }
                else
                {
                    chojongjungBuilder(cho, jung, "");
                    cho = jong;
                    jong = "";
                    textVariable.text = textVariable.text + " ";
                }

                jongPossible = true;
                choPossible = false;
                startMouemBuild = true;
            }

            // 초성 다음 중성 입력 받은 경우
            if (jungPossible)
            {
                if (startMouemBuild)
                {
                    moeum1 = inputText;
                    moeum = moeum1;
                    startMouemBuild = false;
                    if (moeum == "ㅡ" || moeum == "ㅣ") // ㄱ + ㅡ => 그 인 경우
                    {
                        jung = moeum;
                        choPossible = false; // 다음에 들어오는건 종성이다. 
                        jongPossible = true; 
                        hangelPossible = true;
                    }
                    else
                    {
                        choPossible = true;
                        jongPossible = false; // ㄱ + ㆍ => ㄱㆍ 인 경우
                        hangelPossible = false;
                        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length) + moeum;
                    }
                }

                else
                {
                    moeum2 = inputText;
                    moeum = jungBuilder(moeum1, moeum2);
                    if (moeum.Length >= 2 && moeum != "••") // 예를 들면 ㅓ + ㅡ => ㅓㅡ
                    {
                        moeum1 = moeum2;
                        moeum = moeum2;
                        choPossible = true;
                        hangelPossible = false;
                        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length) + moeum;
                        cho = ""; // 초성이 날아간다.
                    }

                    else if (moeum == "ㅓ" || moeum == "ㅗ")
                    {
                        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 1);
                        moeum1 = moeum;
                        jung = moeum;
                        choPossible = false;
                        jongPossible = true;
                        hangelPossible = true;
                    }

                    else if (moeum == "••")
                    {
                        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length) + "•";
                        moeum1 = moeum;
                        choPossible = true;
                        hangelPossible = false;
                    }

                    else if (moeum1 == "••" && moeum.Length == 1) // •• + l => ㅕ 같은 경우
                    {
                        moeum1 = moeum;
                        jung = moeum;
                        // ㄱ•• => ㄱ => 겨
                        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 2); // ㄱ •• ㅣ 모두 지우기
                        choPossible = false;
                        jongPossible = true;
                        hangelPossible = true;
                    }

                    else // moeum 만듦
                    {
                        moeum1 = moeum;
                        jung = moeum;
                        choPossible = false;
                        jongPossible = true;
                        hangelPossible = true;
                    }
                }
            }  else
            {
                // ㅣ 자음 전에 모음부터 들어온 경우
                textVariable.text = textVariable.text + inputText;
                // 그 다음은.? 일단 나중에 처리하자..
                if (startMouemBuild)
                {
                    moeum1 = inputText;
                    moeum = moeum1;
                    startMouemBuild = false;
                    textVariable.text = textVariable.text + inputText;
                }
                else
                {
                    moeum2 = inputText;
                    moeum = jungBuilder(moeum1, moeum2);
                    if (moeum.Length >= 2 && moeum != "••") // 예를 들면 ㅓ + ㅡ => ㅓㅡ
                    {
                        moeum1 = moeum2;
                        moeum = moeum2;
                        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length) + moeum;
                        cho = "";
                    }

                    else if (moeum1 == "••" && moeum.Length == 1) // •• + l => ㅕ 같은 경우
                    {
                        moeum1 = moeum;
                        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 3) + moeum;                  
                    }

                    else // moeum 만듦
                    {
                        moeum1 = moeum;
                        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 3) + moeum;     
                    }
                }
            }
        }
        else
        {
            startMouemBuild = true;
        }

        if (hangelPossible)
        {
            chojongjungBuilder(cho, jung, jong);
        }
    }

    public void englishBuilder(string inputText, int inputTextLength, int keyNumber)
    {
        if (isgoing && inputTextLength == 2)
        {
            count += 1;
            count %= 2;
            inputString = inputText.Substring(count, 1);
            textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 1) + inputString;
        }

        else if (isgoing && inputTextLength == 3)
        {
            count += 1;
            count %= 3;
            inputString = inputText.Substring(count, 1);
            textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 1) + inputString;
        }

        else if (isgoing && inputTextLength == 4)
        {
            count += 1;
            count %= 4;
            inputString = inputText.Substring(count, 1);
            textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 1) + inputString;
        }

        else if (inputText == "space")
        {
            textSpace();
        }

        else
        {
            recentKeyNumber = keyNumber;
            inputString = inputText.Substring(0, 1);
            textVariable.text = textVariable.text + inputString;
            count = 0;
        }
    }

    public void textSpace()
    {
        textVariable = GetComponent<TMP_InputField>();
        textVariable.text = textVariable.text + "_";
        cho = "";
        jung = "";
        jong = "";

        choPossible = true;
        jungPossible = false;
        jongPossible = false;
        jongFirst = true;
        hangelPossible = false;
        startMouemBuild = true;
        count = 0;
    }

    public void backSpace()
    {
        textVariable = GetComponent<TMP_InputField>();

        if (textVariable.text.Length == 0) return;


        textVariable.text = textVariable.text.Substring(0, textVariable.text.Length - 1);
        cho = "";
        jung = "";
        jong = "";

        choPossible = true;
        jungPossible = false;
        jongPossible = false;
        jongFirst = true;
        hangelPossible = false;
        startMouemBuild = true;
        count = 0;
    }

    public void changeLanguage()
    {
        language = !language;
        //Debug.Log(language);
    }

    private bool isContinuous(int keyNumber)
    {
        if(jongFirst && jongPossible)
        {
           jongFirst = false;
        }
        return keyNumber == recentKeyNumber;
    }

}