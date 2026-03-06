[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-2e0aaae1b6195c2367325f4f02e2d04e9abb55f0b24a779b69b11b9e10269abc.svg)](https://classroom.github.com/online_ide?assignment_repo_id=22871674&assignment_repo_type=AssignmentRepo)


# Rune Arcana

![Title](Rune%20Arcana\Assets\Resources\UI\Main%20Title.png)

탑다운 2D 액션 게임이다.  
마을에서 상호작용으로 스테이지에 진입하고, 스테이지를 탐색하며 전투를 진행한다.

---

## 개발 환경

- Engine: Unity 6.3.
- Input: New Input System.
- Platform: Windows.
- Genre: Top-Down Action.

---

## 게임 흐름

1. 타이틀 화면에서 게임을 시작한다.  
2. 마을에서 이동하며 오브젝트와 상호작용한다.  
3. 상호작용(F)으로 스테이지에 진입한다.  
4. 스테이지에서 이동 및 전투를 진행한다.

---

## 조작법

- 이동: WASD
- 공격: 마우스 좌클릭
- 상호작용: F
- 조준: 마우스 포인터 방향

---

## 구현 기능

### UI / 씬 전환
![TitleScene](picture/TitleScene.png)
- 타이틀 UI 구성.
- 버튼 클릭 기반 씬 전환.
- 페이드 애니메이션을 활용한 전환 연출.

### 플레이어
![TitleScene](picture/StageScene.png)
- Rigidbody2D 기반 탑다운 이동.
- Idle / Move 애니메이션 적용.
- 마우스 조준 기반 투사체 발사(화염구).

### 투사체
- 발사 방향/속도 기반 이동.
- 수명(lifeTime) 기반 자동 파괴.
- 적 충돌 시 파괴 처리.
- 발사/충돌 사운드 재생.

### 몬스터
- 플레이어 추적 이동.
- 투사체 피격 시 HP 감소 및 사망 처리.
- 공격 범위/데미지 스탯 기반 처리.

### 환경/연출
![TitleScene](picture/TownScene.png)
- Sorting Layer 제어로 전경/후경 표현.
- Light2D 기반 캠프파이어 불빛 연출.

---

## 데이터 구조

- ScriptableObject 기반 스탯 데이터 분리.
  - PlayerStat: HP, Damage, MoveSpeed.
  - MonsterStat: HP, MoveSpeed, Attack_Range, Attack_Damage 등.

---

## 폴더 구조

- Assets/Scripts/
  - Manager/
  - Player/
  - Monster/
  - UI/
  - Props/
  - Util/

---

## 트러블슈팅

- Animator가 루트 Transform을 커브로 제어하면 Rigidbody2D 이동이 무력화될 수 있다.  
  Root(물리)와 Visual(애니메이션)을 분리하는 방식이 안정적이다.
- LayerMask는 Raycast에 사용할 때 비트마스크 형태로 세팅해야 한다.  
  LayerMask.GetMask 또는 1 << layerIndex 형태로 사용한다.
- AudioSource/Clip이 누락되면 투사체 사운드에서 예외가 발생할 수 있다.  
  null 체크와 자동 컴포넌트 확보로 방어한다.

---

## 앞으로 개선할 점

- 방 단위 전투 트리거 및 문 잠금/해제 구현.
- 몬스터 공격 패턴 다양화 및 플레이어 피격/무적 시간 처리.
- 투사체/이펙트 Object Pool 적용.
- 게임 클리어/게임 오버 UI 정리.

---

## 실행 방법

1. Unity에서 프로젝트를 연다.
2. Build Settings에 씬을 등록한다.
3. Play 또는 Build 후 실행한다.

---