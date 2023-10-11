프로그램 사양서

 

프로젝트명 : 문자 전송 서버 개발                                                   작성자 : 이재원

작성일 : 2023/10/11

 

 

 

-개발 언어 : C#

-파일명 : prohect_SMS.sln

-파일 형태 : WinForm

-C# 버전 : C#8.0

-프레임워크 : .NET8.0

 

-프로젝트 설명

TCP 소켓 통신 기반

서버 생성 : Port 번호를 지정 받아 서버를 생성 후 LISTEN 상태로 대기



서버 접속 : Client 해당 Server Port로 접속하여 서버에 JSON 형식으로 메시지를 보낼 경우 전화번호와 내용을 확인하여 NAVER SENS API 연동을 통해 해당 전화번호로 문자를 보내는 프로그램.

발신 번호는 해당 Cloud에 등록한 번호로만 발신이 가능 “010-4873-4882”으로 고정

-JSON 형식 전송

1)SMS SEND

필수 입력 값 : tel , content

입력 format : “tel” : “xxx-xxxx-xxxx”,”content”:”[내용]”

유효성 검사 : 필수입력값 검사, 전화번호 형식 검사

 

2)LMS SEND

조건 : content 값이 80 btye 초과 2000byte 이하 일때 LMS 전송

필수 입력 값 : subject, tel, content

입력 format : “subject”:”[제목]”,”tel”:”xxx-xxxx-xxxx”,”content”:”[내용]”

유효성 검사 : 필수 입력값 검사, 전화번호 형식 검사

 

-전송 요청 완료

Client 에게 JSON 형식으로 결과 전송


-전송 요청 실패
