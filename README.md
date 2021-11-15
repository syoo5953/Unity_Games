# Unity_Game

개인 게임 작업물 레퍼지토리.

**3D RPG 게임 (포트폴리오용)**
- 작업기간: 총 5일
- 목표: 취업 준비 시간 외 여유 시간을 활용하여 퀘스트 + 인벤토리 및 몬스터 추가 작업 -> 포톤을 활용하여 멀티플레이 기능 추가.
- 목표 완수까지 예상 기간: 2주 내.

 **설명**

- Enemy Creation에 Object Pooling을 사용하여 가비지컬렉터 비용 최소화.
- NavMeshAgent를 활용하여 Enemy가 플레이어를 추적.
- RayCast를 활용하여 플레이어가 Enemy의 사정거리 밖으로 나갈경우 Enemy는 제자리로 돌아가도록 구현.
- 카메라 Zoom in & out 기능 및 미니맵 구현.
- 인벤토리 (still 구현중).
- 다음 맵으로 이동 전 맵 로딩 시간을 위한 Map Loading Scene 구현 (SceneManager.LoadSceneAsync 활용).
- 씬 이동 시 DontDestroy를 활용하여 캐릭터 및 주요 정보 유지.

#### 포트폴리오용 동영상 Path

Unity_Games/유니티 데모버전 테스트용 게임 플레이영상/포트폴리오 짧은 영상 - 3D RPG 게임.mp4
