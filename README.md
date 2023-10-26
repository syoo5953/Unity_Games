# Unity_Game 개인 게임 작업물 레퍼지토리.

![캡처](https://user-images.githubusercontent.com/92070358/146652568-a48bc0a8-3289-41b9-85b8-bd58d5381916.PNG)

![캡처1](https://user-images.githubusercontent.com/92070358/146652573-670c8cdc-2300-4fce-9059-0ce2e686a508.PNG)

![캡처2](https://user-images.githubusercontent.com/92070358/146652575-6bd83ce7-bb5d-43dc-89c4-7d1c3aa06737.PNG)


## 3D RPG 게임 (포트폴리오용)
- **작업기간**: 총 5일
- **목표**: 취업 준비 시간 외 여유 시간을 활용하여 퀘스트 + 인벤토리 및 몬스터 추가 작업 -> 포톤을 활용하여 멀티플레이 기능 추가.
- **목표 완수까지 예상 기간**: 2주 내.

### 설명
- Enemy Creation에 Object Pooling을 사용하여 가비지컬렉터 비용 최소화.
- NavMeshAgent를 활용하여 Enemy가 플레이어를 추적.
- RayCast를 활용하여 플레이어가 Enemy의 사정거리 밖으로 나갈경우 Enemy는 제자리로 돌아가도록 구현.
- 카메라 Zoom in & out 기능 및 미니맵 구현.
- 인벤토리 (still 구현중).
- 다음 맵으로 이동 전 맵 로딩 시간을 위한 Map Loading Scene 구현 (SceneManager.LoadSceneAsync 활용).
- 씬 이동 시 DontDestroy를 활용하여 캐릭터 및 주요 정보 유지.

##### 포트폴리오 동영상 (링크 접속 후 view raw 클릭)

[포트폴리오용 동영상](https://github.com/syoo5953/Unity_Games/blob/main/%EC%9C%A0%EB%8B%88%ED%8B%B0%20%EB%8D%B0%EB%AA%A8%EB%B2%84%EC%A0%84%20%ED%85%8C%EC%8A%A4%ED%8A%B8%EC%9A%A9%20%EA%B2%8C%EC%9E%84%20%ED%94%8C%EB%A0%88%EC%9D%B4%EC%98%81%EC%83%81/%ED%8F%AC%ED%8A%B8%ED%8F%B4%EB%A6%AC%EC%98%A4%20%EC%A7%A7%EC%9D%80%20%EC%98%81%EC%83%81%20-%203D%20RPG%20%EA%B2%8C%EC%9E%84.mp4)

---

## 2021-11-25 모바일 전환 및 연동 완료

### 설명 
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

### 추가해야할 사항들

1. 보스 구현
2. 전체적 UI 개선
3. 이전 버전의 카메라 Zoom in & out 기능 및 미니맵 재구현.

---

## 2021-12-12 부수적인 기능들 구현완료

1. 보스 구현완료 (보스 점프, 스킬 등 행동 추가 예정)
2. 슬롯 물약 장착기능 완료
3. ETC...

---

## 2021-12-19 부수적인 기능들 구현완료

1. 스킬 및 물리효과 개선 진행중

##### 포트폴리오 동영상 (링크 접속 후 view raw 클릭)

[포트폴리오용 동영상](https://github.com/syoo5953/Unity_Games/blob/main/%EC%9C%A0%EB%8B%88%ED%8B%B0%20%EB%8D%B0%EB%AA%A8%EB%B2%84%EC%A0%84%20%ED%85%8C%EC%8A%A4%ED%8A%B8%EC%9A%A9%20%EA%B2%8C%EC%9E%84%20%ED%94%8C%EB%A0%88%EC%9D%B4%EC%98%81%EC%83%81/(%EB%AA%A8%EB%B0%94%EC%9D%BC%20%EC%A0%84%ED%99%98)%20%ED%8F%AC%ED%8A%B8%ED%8F%B4%EB%A6%AC%EC%98%A4%20%EC%98%81%EC%83%81%20-%203D%20RPG%20%EA%B2%8C%EC%9E%84.mp4)

---

## 2021-12-19 부수적인 기능들 구현완료
1. 라이트 프로빙과 리플렉션 등을 활용한 랜더링 개선
2. MMORPG 다운 게임을 위한 추가적인 기능추가 및 개선 진행중

완성 후 동영상 링크 업로드 예정.

## 2022-01-03 URP로 전환중. UI 개선 및 모바일 최적화 끊임없이 진행중.
[URP 전환중인 동영상](https://github.com/syoo5953/Unity_Games/blob/main/%EC%9C%A0%EB%8B%88%ED%8B%B0%20%EB%8D%B0%EB%AA%A8%EB%B2%84%EC%A0%84%20%ED%85%8C%EC%8A%A4%ED%8A%B8%EC%9A%A9%20%EA%B2%8C%EC%9E%84%20%ED%94%8C%EB%A0%88%EC%9D%B4%EC%98%81%EC%83%81/%EB%AA%A8%EB%B0%94%EC%9D%BC%20RPG%20%EA%B2%8C%EC%9E%84.mp4)

---

## 2022-01-16.
- 적이 사정거리 안에 들어올 시 가까이 돌진하여 공격
- 인벤토리 수정

[본격적인 최적화 & 기능구현 시작 동영상](https://github.com/syoo5953/Unity_Games/blob/main/%EC%9C%A0%EB%8B%88%ED%8B%B0%20%EB%8D%B0%EB%AA%A8%EB%B2%84%EC%A0%84%20%ED%85%8C%EC%8A%A4%ED%8A%B8%EC%9A%A9%20%EA%B2%8C%EC%9E%84%20%ED%94%8C%EB%A0%88%EC%9D%B4%EC%98%81%EC%83%81/(%EB%AA%A8%EB%B0%94%EC%9D%BC%20%26%20URP%20%EC%A0%84%ED%99%98)%20%ED%8F%AC%ED%8A%B8%ED%8F%B4%EB%A6%AC%EC%98%A4%20%EC%98%81%EC%83%81%20-%203D%20RPG%20%EA%B2%8C%EC%9E%84.mp4)

---

## 2022-02-01 쉐이더 학습중
- 쉐이더 그래프를 활용한 카둔랜더링 적용 (Is Front Face의 값을 Flip, 외각선의 Thickness 값을 활용)

![캡처](https://user-images.githubusercontent.com/92070358/151821474-7350168a-cc8d-4463-8fc4-4a341aeb78eb.PNG)

---

## 2022-09-26 기존 RPG 프로젝트 재구축
- 모바일 전환 시 끊김 현상 지속적으로 발생.
- Profiler를 통해 랜더링 및 스크립트에서 부하가 걸린다는 것을 파악.
- 현재까지 쌓인 지식을 토대로 최적화와 개발 동시 진행.

---

## 2023-09-05 게임 기획 및 개발 준비중(기획자 2, 디자이너 1, 개발(본인) 1)

![image](https://github.com/syoo5953/Unity_Games/assets/92070358/a3f49d6d-eeda-45e5-b671-a253b18b63f3)

- Plastic SCM을 활용한 Unity 공동 작업 구성 완료.
- 기획 단계 완료. 기획자와 좀 더 커뮤니케이션 및 보완 작업 후 업데이트 예정.
- 디펜스 게임이니 만큼, 많은 몬스터 spawn 예정. 이를 위한 Object Pooling 사용 예정.
- 접근성, 종속성, 유일성을 위해 필요한 오브젝트의 한해서 Singleton 스크립트 적용 예정.


### 로비 프로토타입 영상[디자이너 협의 후 UI 변경 예정. 지금은 테스트용.]

![ezgif-4-b97c0c59f1](https://github.com/syoo5953/Unity_Games/assets/92070358/c07da41b-12ab-4b9d-8951-2e2d1db1d6ee)


### 시각적 심심함 때문에, 나뭇잎이 떨어지는 Particle System 제작

![image](https://github.com/syoo5953/Unity_Games/assets/92070358/a4163d18-bbf6-4509-8761-b31496a69349)

### Wave system 구현 TEST

![robot](https://github.com/syoo5953/Unity_Games/assets/92070358/adccf73f-7c10-4a06-9952-bde024f0807d)

### 설명
- CSM AI를 활용하여 2D -> 3D 변환.
- Mixamo를 통해 3D 리깅 및 애니메이션 추출.
- Object Pooling을 통해 GC 비용 개선.
- Singleton 패턴 사용.
