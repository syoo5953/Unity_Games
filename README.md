# Unity_Game

![image1](https://user-images.githubusercontent.com/92070358/143458615-bbb8cb48-152a-4be5-89d6-1a04a918962f.PNG)

![image2](https://user-images.githubusercontent.com/92070358/143458649-866e4066-48a6-4cb8-9723-9dff1c4cc39b.PNG)


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

##### 포트폴리오 동영상 (링크 접속 후 view raw 클릭)

[포트폴리오용 동영상](https://github.com/syoo5953/Unity_Games/blob/main/%EC%9C%A0%EB%8B%88%ED%8B%B0%20%EB%8D%B0%EB%AA%A8%EB%B2%84%EC%A0%84%20%ED%85%8C%EC%8A%A4%ED%8A%B8%EC%9A%A9%20%EA%B2%8C%EC%9E%84%20%ED%94%8C%EB%A0%88%EC%9D%B4%EC%98%81%EC%83%81/%ED%8F%AC%ED%8A%B8%ED%8F%B4%EB%A6%AC%EC%98%A4%20%EC%A7%A7%EC%9D%80%20%EC%98%81%EC%83%81%20-%203D%20RPG%20%EA%B2%8C%EC%9E%84.mp4)


### 2021-11-25 모바일 전환 및 연동 완료

 **설명**
 
- Joystick으로 대체
- Enemy Creation에 Object Pooling을 사용하여 가비지컬렉터 비용 최소화.
- Particle, Canvas 등 Object Pooling 추가 (Instantiate 대신 SetActive(true/false)로 수정)
- NavMeshAgent를 활용하여 Enemy가 플레이어를 추적.
- RayCast를 활용하여 플레이어가 Enemy의 사정거리 밖으로 나갈경우 Enemy는 제자리로 돌아가도록 구현.
- 다음 맵으로 이동 전 맵 로딩 시간을 위한 Map Loading Scene 구현 (SceneManager.LoadSceneAsync 활용).
- 씬 이동 시 DontDestroy를 활용하여 캐릭터 및 주요 정보 유지.
- Post Processing을 활용한 Antialiasing (오브젝트의 계단식 랜더링을 부드럽게 개선)
- 퀘스트 UI 추가완료
- 인벤토리 추가완료
- 부활, 힐 등 기타 기능 구현완료

###### 추가해야할 사항들

1. 보스 구현
2. 전체적 UI 개선
3. 이전 버전의 카메라 Zoom in & out 기능 및 미니맵 재구현.

### 2021-12-12 부수적인 기능들 구현완료

1. 보스 구현완료 (보스 점프, 스킬 등 행동 추가 예정)
2. 슬롯 물약 장착기능 완료
3. ETC...

##### 포트폴리오 동영상 (링크 접속 후 view raw 클릭)

[포트폴리오용 동영상](https://github.com/syoo5953/Unity_Games/blob/main/%EC%9C%A0%EB%8B%88%ED%8B%B0%20%EB%8D%B0%EB%AA%A8%EB%B2%84%EC%A0%84%20%ED%85%8C%EC%8A%A4%ED%8A%B8%EC%9A%A9%20%EA%B2%8C%EC%9E%84%20%ED%94%8C%EB%A0%88%EC%9D%B4%EC%98%81%EC%83%81/(%EB%AA%A8%EB%B0%94%EC%9D%BC%20%EC%A0%84%ED%99%98)%20%ED%8F%AC%ED%8A%B8%ED%8F%B4%EB%A6%AC%EC%98%A4%20%EC%A7%A7%EC%9D%80%20%EC%98%81%EC%83%81%20-%203D%20RPG%20%EA%B2%8C%EC%9E%84.mp4)
