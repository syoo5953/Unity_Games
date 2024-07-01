## 목차
1. [2023-09-05: 게임 기획 및 개발](#2023-09-05-게임-기획-및-개발)
2. [2024-01-23](#2024-01-23)
3. [2024-02-13](#2024-02-13)
4. [2024-02-25](#2024-02-25)
5. [2024-04-09](#2024-04-09)
6. [2024-07-01](#2024-07-01)

## 2023-09-05: 게임 기획 및 개발
### 개요
- **Plastic SCM**: Unity 협업 구성 완료.
- **기획 단계**: 완료. 기획자와 추가 커뮤니케이션 및 보완 작업 중.
- **디펜스 게임**: 많은 몬스터 스폰 예정. Object Pooling 사용 예정.
- **Singleton 패턴**: 접근성, 종속성, 유일성을 위해 적용.

### 로비 프로토타입 영상
![로비 프로토타입](https://github.com/syoo5953/Unity_Games/assets/92070358/c07da41b-12ab-4b9d-8951-2e2d1db1d6ee)
*디자이너 협의 후 업데이트 예정. 현재 UI는 테스트용.*

### 시각적 개선
#### 나뭇잎 떨어지는 파티클 시스템
![파티클 시스템](https://github.com/syoo5953/Unity_Games/assets/92070358/a4163d18-bbf6-4509-8761-b31496a69349)

### 웨이브 시스템 테스트
![웨이브 시스템 테스트](https://github.com/syoo5953/Unity_Games/assets/92070358/adccf73f-7c10-4a06-9952-bde024f0807d)

### 설명
- **CSM AI**: 2D -> 3D 변환에 사용.
- **Mixamo**: 3D 리깅 및 애니메이션 추출.
- **Object Pooling**: GC 비용 개선.
- **Singleton 패턴**: 사용.

### 주요 사항
- **독립적인 씬 구성**: 모든 씬을 독립적으로 구성. 메인 씬에서 게임 시작 시 웨이브 시스템 문제 없음.
- **데이터 관리**: 기획자가 쉽게 수정할 수 있도록 CSV로 데이터 관리. DataManager에서 데이터 로드. 암호화 작업 예정.
- **디자인 패턴**: 새로운 공격 타입, 적, 히어로 등을 유연하게 추가/제거할 수 있도록 전략 패턴 등 사용.
- **이벤트 처리**: Unity Action 또는 Action events 사용. 유연한 함수 실행 구현. Destroy 시 이벤트 구독 취소 필수.
- **OOP 원칙**: 스크립트는 OOP SOLID 원칙 준수. 목적과 구별성 명확, 유연하게 확장 가능, 의존성 주입 가능하도록 설계.
- **타겟팅**: 비용 효율성을 위해 OnTriggerEnter/Stay/Exit 사용.

### 시각적 효과
![시각적 효과](https://github.com/syoo5953/Unity_Games/assets/92070358/a1dcfc90-b8fc-46d5-9812-440830a48ee2)

### 데이터 로딩
![데이터 로딩](https://github.com/syoo5953/Unity_Games/assets/92070358/7cb42fd8-a217-4ad7-b96d-9dfe9f6c20fe)
- 데이터 로드 시간에 비례하여 로딩 바 증가.
- 모든 데이터 최초 1회 로드 완료 후 메인 씬으로 전환.
- 게임 실행 시에는 Lazy Load로 데이터 로드.
- ScriptableObject HeroData와 EnemyData 리스트에 저장.

### 풀링 시스템
![풀링 시스템](https://github.com/syoo5953/Unity_Games/assets/92070358/755ba806-7a99-4514-ae91-383e5a756935)
- 파티클, 적 스폰 등을 효율적으로 관리.
- 미리 생성된 오브젝트를 스폰하고 풀로 반환.

### 공격 시스템
![공격 시스템](https://github.com/syoo5953/Unity_Games/assets/92070358/5f32e995-acdc-482d-8289-436b8f58d79c)
- IAttackBehaviour, IDamageable, enum AttackType 등을 구현하여 코드 간결화 및 유연한 확장성 구현.

---

## 2024-01-23

### Addressable Asset 구현
![Addressable Assets](https://github.com/syoo5953/Unity_Games/assets/92070358/400f23ef-7fdb-4428-8e7f-acd7f3eb69e6)
- 로딩 속도 향상 및 빌드 크기 감소를 위해 고려 중.
- 자산의 의존성과 버전 관리를 자동화.
- 텍스처 최적화를 통해 빌드 크기 축소 예정.
- 기획자와 협력하여 모델 및 UI 제작 중. 시간이 많이 소요되면 프리랜서 의뢰 예정.

---

## 2024-02-13

### 업데이트
- **Addressable Asset**: 적용 완료.
- **조명**: Lightmap 및 베이킹 완료.
- **드로우콜 최적화**: 4000에서 200으로 감소.
- **옵션 시스템**: 오디오, 그래픽 등 옵션 시스템 구현 완료.
- **CSV to JSON 변환**: 비개발자도 쉽게 데이터 수정 가능.
- **히어로 효과**: 소환 시 Dissolve Effect 구현 완료.
- **구매/판매 시스템**: 히어로 구매 및 판매 시스템 구현 완료.

### 향후 계획
- **네트워킹**: Photon을 사용하여 국가 간 룸 생성 및 참여 기능 구현 예정.
- **스킬 구현**: Warrior 스킬은 Projectile과 다르게 구현 필요.
- **리소스 확보**: 맵, 캐릭터, 몬스터 등 리소스 확보 예정.
- **컷신**: 몬스터 출현, 시민 도망, 마법사 보호막 생성 등 디펜스 시작 장면 구현 예정.

---

## 2024-02-25

### AI 및 시스템 개선
![AI 시스템 개선](https://github.com/syoo5953/Unity_Games/assets/92070358/307ae11f-b395-43df-b34f-ba5e7fa763a8)
- **Navmesh Agent AI**: 장애물 회피 기능 개선.
- **판매 시스템**: 구현 완료.
- **상태 표시줄**: 구현 중.

### 경로 찾기 개선
![경로 찾기 개선](https://github.com/syoo5953/Unity_Games/assets/92070358/9433be00-34de-4aca-908d-22f24ff8e25b)
1. 장애물 회피 기능.
2. 타겟 지점 근처에서의 회전 이슈 해결.
3. 탐지 각도 넓힘.
4. 유닛 밀침 문제 해결.

### 플로팅 아일랜드 평탄화
![플로팅 아일랜드](https://github.com/syoo5953/Unity_Games/assets/92070358/86f05ebd-0c7b-4175-8756-66011f179f55)

---

## 2024-04-09

### 스킬 시스템
![스킬 시스템](https://github.com/syoo5953/Unity_Games/assets/92070358/c8ad3013-15f5-48a0-bdd5-4edde35d9961)
- JSON에서 쉽게 변경할 수 있도록 스킬 구성.

### 맵 완성
![맵 디자인](https://github.com/syoo5953/Unity_Games/assets/92070358/17e28a73-ba33-492a-90ed-9d7dd7b78d36)

### 당장 해야 할 일
- 캐릭터 자산 구매.
- 프레임 속도 개선 (프로파일러 사용).
- 게임 분위기 변경 (LOL 느낌으로).
- 매치메이킹 구현 (Photon Fusion2 사용 예정).
- 적절한 Skybox 확보.
- 디자이너에게 받은 UI 디자인 적용.

### 베지어 곡선 소환 효과
![베지어 곡선](https://github.com/syoo5953/Unity_Games/assets/92070358/7b1cc578-fdf4-45c2-a342-2116c720b0c8)

---

## 2024-07-01

### 멀티플레이 및 카메라 업데이트
![멀티플레이 및 카메라](https://github.com/syoo5953/Unity_Games/assets/92070358/f15984d7-49bb-471f-9fcd-7feccf038a78)
- **멀티플레이어**: 구현 중.
- **카메라 전환**:
