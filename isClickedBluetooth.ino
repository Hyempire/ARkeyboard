#include <SoftwareSerial.h> //시리얼통신 라이브러리 호출
 
int blueTx=9;   //Tx (보내는핀 설정)at
int blueRx=8;   //Rx (받는핀 설정)
SoftwareSerial mySerial(blueRx, blueTx);  //시리얼 통신을 위한 객체선언

int baudRateNum = 9600;

int sensor = A0;
int count = 0;
int debounce = 0;
int previousValue;
int currentValue;
int threshold = 10;

bool isTriggered = false;

void setup() {
  Serial.begin(baudRateNum);   //시리얼모니터
  mySerial.begin(baudRateNum); //블루투스 시리얼
}

void loop() {
  int value = analogRead(sensor);
  int printValue = 0;

  if (value >= threshold){
    //Serial.print(currentValue - previousValue);
    if (isTriggered == false){
      printValue = 1;
      mySerial.println(printValue);
      isTriggered = true;
    }
    else {
      printValue = 0;
      mySerial.println(printValue);
      isTriggered = true;
    }
  }
  else {
    printValue = 0;
    mySerial.println(printValue);
    isTriggered = false;
  }
  //mySerial.println(printValue);
  delay(70);
}
