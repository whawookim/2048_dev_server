# Unity 2048 Game – Backend Server

본 프로젝트는 Unity 2048 게임의 백엔드 서버입니다.  
간단한 로그인, 랭킹 등록/조회 등의 API를 제공합니다.

## 🛠️ 사용 기술

- ASP.NET Core 8.0
- Entity Framework Core
- PostgreSQL
- JSON 기반 REST API

## 📦 주요 기능

- 사용자 로그인 (게스트 / Google IDP 연동 구조 고려)
- 사용자 닉네임 등록 및 수정
- 랭킹 등록 및 조회 API
- 유저 ID 기반 서버 연동 구조
- HTTPS 기반 API 통신 지원
- 구조화된 API 통신 방식

## 🔧 실행 방법

1. `appsettings.json`에서 DB 연결 문자열을 설정합니다.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=yourpassword;Database=game2048db"
  }
}
```

2. 마이그레이션 및 DB 초기화

```bash
dotnet ef database update
```

3. 서버 실행

```bash
dotnet run
```

## 🔗 관련 레포지토리

- Unity 클라이언트 레포: [2048_dev_cli](https://github.com/whawookim/2048_dev_cli)

## 📌 비고

- 테스트 및 포트폴리오 용도로 제작된 서버입니다.
- 인증 및 보안 처리는 최소화된 상태이며, 상용 목적이 아닙니다.
