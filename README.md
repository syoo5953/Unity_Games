# 목차
1. [2024-04-05: 게임 기획 및 개발](#2024-04-05-게임-기획-및-개발)
2. [2024-04-23](#2024-04-23)
3. [2024-05-13](#2024-05-13)
4. [2024-05-25](#2024-05-25)
5. [2024-06-09](#2024-06-09)
6. [2024-06-23](#2024-06-23)
7. [2024-07-25](#2024-07-25)
8. [2024-08-01](#2024-08-01)
9. [2024-08-10](#2024-08-10)
10. [2024-08-29](#2024-08-29)
11. [2024-10-03](#2024-10-03)
12. [2024-10-22](#2024-10-22)
13. [번외. 첫 포트폴리오 작품](#번외-첫-포트폴리오-작품)

## 2024-04-05: 게임 기획 및 개발
### 개요
- **출시**: 개발에 허용되는 자유 시간이 많지 않음. 출시 기간은 2024년 10월 ~ 11월로 예정 중(테스트 기한 한달 포함).
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
- **데이터 관리**: 기획자가 쉽게 수정할 수 있도록 CSV로 데이터 관리. DataManager에서 데이터 로드. 암호화 작업 예정.
- **디자인 패턴**: 새로운 공격 타입, 적, 히어로 등을 유연하게 추가/제거할 수 있도록 전략 패턴 등 사용.
- **이벤트 처리**: Unity Action 또는 Action events 사용. 유연한 함수 실행 구현. Destroy 시 이벤트 구독 취소 필수.
- **OOP 원칙**: 스크립트는 OOP SOLID 원칙 준수. 목적과 구별성 명확, 유연하게 확장 가능, 의존성 주입 가능하도록 설계.
- **타겟팅**: 비용 효율성을 위해 OnTriggerEnter/Stay/Exit에서 Physics.OverlapSphere로 대체.

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

## 2024-04-23

### Addressable Asset 구현
![Addressable Assets](https://github.com/syoo5953/Unity_Games/assets/92070358/400f23ef-7fdb-4428-8e7f-acd7f3eb69e6)
- 로딩 속도 향상 및 빌드 크기 감소를 위해 고려 중.
- 자산의 의존성과 버전 관리를 자동화.
- 텍스처 최적화를 통해 빌드 크기 축소 예정.
- 기획자와 협력하여 모델 및 UI 제작 중. 시간이 많이 소요되면 프리랜서 의뢰 예정.

---

## 2024-05-13

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

## 2024-05-25

### AI 및 시스템 개선
[AI 및 시스템 개선 비디오](https://github.com/syoo5953/Unity_Games/assets/92070358/307ae11f-b395-43df-b34f-ba5e7fa763a8)
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

## 2024-06-09

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

### Beizer Curve 소환 효과
[Beizer Curve 소환 효과 비디오](https://github.com/syoo5953/Unity_Games/assets/92070358/7b1cc578-fdf4-45c2-a342-2116c720b0c8)

---

## 2024-06-23

### 멀티플레이 및 카메라 업데이트
[멀티플레이 및 카메라 업데이트 비디오](https://github.com/syoo5953/Unity_Games/assets/92070358/f15984d7-49bb-471f-9fcd-7feccf038a78)
- **멀티플레이어**: 구현 중. 렉 보간을 활용하여 스무스한 싱크 구현 완료(RPC 전송 방식 및 네트워크 로직 수정이 많이 필요함).
- **카메라 전환 기능**: 구현 완료
- **내부 요소 전체적으로 수정**: 멀티플레이 도입에 맞춰 데이터 로드부터 씬의 dependancy 등 전체적으로 수정.
- **UI 관련 진행상황**: UI는 새로운 외주가 시안 작업 중.

<details>
  <summary>HeroController.cs 베이스 클래스 -> 각 Hero 타입 별 클래스 코드 수정</summary>

```csharp
using Photon.Pun;
using UnityEngine;

public class MageHero : HeroController
{
    private float lastAttackTime = 0;
    private AudioManager audioManager;
    private PhotonView targetPhotonView;
    private ParticleData baseAttackData;
    private ParticleData skillAttackData;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        baseAttackData = DataLoader.Instance.GetParticleData(baseAttack);
        skillAttackData = DataLoader.Instance.GetParticleData(skillAttack);
    }

    protected override void TryAttack()
    {
        if (mpSlider != null && mpSlider.value >= 1.0f)
        {
            FaceTarget(true);
            LaunchAttack(true);
            mpSlider.value = 0;
            skillChargeTime = 0;
        }
        else if (Time.time - lastAttackTime >= 1 / attackSpeed)
        {
            FaceTarget(true);
            LaunchAttack(false);
            lastAttackTime = Time.time;
        }
    }

    private void LaunchAttack(bool isSkill)
    {
        if (target != null)
        {
            targetPhotonView = target.GetComponent<PhotonView>();
            int targetID = targetPhotonView.ViewID;
            if (isSkill)
            {
                FireSkill(targetID);
                photonView.RPC("FireSkill", RpcTarget.Others, targetID);
                audioManager.Play(skillAttackAudio);
            }
            else
            {
                Fire(targetID);
                photonView.RPC("Fire", RpcTarget.Others, targetID);
                audioManager.Play(baseAttackAudio);
            }
        }
    }

    [PunRPC]
    private void Fire(int targetID)
    {
        ExecuteFire(targetID, baseAttackData, baseAttackDamage);
    }

    [PunRPC]
    private void FireSkill(int targetID)
    {
        ExecuteFire(targetID, skillAttackData, skillAttackDamage);
    }

    private void ExecuteFire(int targetID, ParticleData attackData, float damage)
    {
        PhotonView targetPhotonView = PhotonView.Find(targetID);
        if (targetPhotonView != null)
        {
            Transform targetTransform = targetPhotonView.transform;
            if (attackData != null)
            {
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackSpeed", attackSpeed);

                ParticleExecutor.ExecuteParticle(damage, attackRange, transform, targetTransform, attackData);
            }
        }
    }
}
```
</details>

---

## 2024-07-25
- 멀티플레이 구현 완료 (이제 씽크가 잘 맞고, RPC도 최소한으로 호출하여 부하를 줄였다!)
  -- 방 생성 후 진입 시 fade in and out -> Player 대기 -> 플레이어 준비 완료 시 게임 시작 전 5초 카운트 다운 실시도 이제 잘 된다.
- 로비에서 방 코드(4개의 숫자)로 조인하는 private 룸 기능 추가
- hero states 수정 완료(공격, 움직임, 적감지 등)
- 모든 플레이어 동의 시 2배속 변경 (한명이라도 체크를 풀면 1x로 다시 변경)
- 스킬(버프/디버프) / 게임 요서(카드 선택 및 효과 부여) 등 추가중...
- 몬스터 및 영웅 에셋 추가중... (구현을 잘 해놓은 덕분에 추가하는데 애니메이션(이미 있을 경우), 공격, 스킬 포함 개당 2~3분정도 소요)
- 3개의 카드 Pick 시스템 추가중...

https://github.com/user-attachments/assets/129b0de3-60f8-41d2-8bc7-f7b3e927b875

<details>
  <summary>UpgradeManager 코드 추가</summary>

```csharp
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public static int MagicianLevel { get; private set; } = 0;
    public static int ArcherLevel { get; private set; } = 0;
    public static int WarriorLevel { get; private set; } = 0;

    public static event Action<HeroType> OnHeroLevelUp;

    public void UpgradeHeroes(HeroType heroType)
    {
        switch (heroType)
        {
            case HeroType.Magician:
                MagicianLevel++;
                break;
            case HeroType.Archer:
                ArcherLevel++;
                break;
            case HeroType.Warrior:
                WarriorLevel++;
                break;
        }

        OnHeroLevelUp?.Invoke(heroType);
        
        var heroesToUpgrade = UnitSelection.Instance.unitList
            .Where(hero => hero.GetComponent<HeroController>().heroData.heroType == heroType)
            .ToList();

        foreach (var hero in heroesToUpgrade)
        {
            var levelUpEffect = ObjectPoolManager.Instance.GetObjectFromPool("levelUp", hero.transform.position, Quaternion.identity);
            StartCoroutine(ReturnObjectToPoolAfterDelay(levelUpEffect, 1f));
        }
    }

    public static int GetHeroLevel(HeroType heroType)
    {
        return heroType switch
        {
            HeroType.Magician => MagicianLevel,
            HeroType.Archer => ArcherLevel,
            HeroType.Warrior => WarriorLevel,
            _ => 0
        };
    }

    private IEnumerator ReturnObjectToPoolAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ObjectPoolManager.Instance.ReturnObjectToPool(obj);
    }
}
```
</details>

## 2024-08-01
https://github.com/user-attachments/assets/2646c33d-39ab-4b00-9dad-9fda6b2b992b

![image](https://github.com/user-attachments/assets/6e4cdb3b-5d9b-41ae-8f6f-7be9376b1c32)
- Drop Gemstone 추가
- 데미지 팝업 추가
- 캐릭터 및 스킬 계속 추가중...

## 2024-08-10
https://github.com/user-attachments/assets/e5139b45-2a5f-4ef5-93da-ff8316643a22
- 카드 Ability Scribtable Objects 생성 완료
- 카드 UI 및 쉐이더 이펙트 구성중

https://github.com/user-attachments/assets/991ddded-bc30-446d-b721-8a91bf32fff4

## 2024-08-29

https://github.com/user-attachments/assets/ecdab700-fcc3-4607-942d-1f9badb5f117

- 멀티플레이 안정화 작업 완료
- 스킬 구현 완료(Legendary등급은 3번 공격 시 Side Effect 발동)
- 잔버그 수정
- 공격 로직 전부 수정

<details>
  <summary>GameManager 멀티플레이 로직 수정</summary>

```csharp
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance { get; private set; }
    public static PlayerManager PlayerManager { get; private set; }

    ... variables ...

    private UIManager uiManager;

    public const string PLAYER_READY = "IsPlayerReady";
    public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";
    public const string PLAYER_KILLS = "PlayerKills";

    private void Awake()
    {
        Instance = this;
        uiManager = FindObjectOfType<UIManager>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.InRoom)
            {
                PlayerIndexHelper.Instance.InitializePlayerIndexMap();
                EnvironmentSettings.Instance.InitializeSettings();
                GameObject playerObj = PhotonNetwork.Instantiate("PlayerPrefab", Vector3.zero, Quaternion.identity);
                PlayerIndexHelper.Instance.AddPlayerInputManager(playerObj, PhotonNetwork.LocalPlayer.ActorNumber);
                PlayerManager = FindLocalPlayerManager();
                PlayerManager.Initialize();
            }
            else
            {
                Debug.LogWarning("Player is not in a room.");
            }
        }
        else
        {
            Debug.LogWarning("Client is not connected and ready.");
        }

        StartCoroutine(FadeAndStartCountdown());
    }

    private IEnumerator FadeAndStartCountdown()
    {
        yield return new WaitForSeconds(1);
        FadeManager.Instance.FadeOut(() =>
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { PLAYER_LOADED_LEVEL, true }, { "IsReady", false } });
        });

        // Fadeout 끝날때까지 기다리기...
        yield return new WaitUntil(() => !FadeManager.Instance.IsFading);
        int playerIndex = PlayerIndexHelper.Instance.GetPlayerIndex(PhotonNetwork.LocalPlayer.ActorNumber);
        CutsceneManager.Instance.StartCutscene(playerIndex);
    }

    public override void OnLeftRoom()
    {
        SpawnManager.Instance.StopSpawningEnemies();
        PhotonNetwork.Disconnect();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        otherPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "IsReady", false } });
        EnvironmentSettings.Instance.OnPlayerLeft(PlayerIndexHelper.Instance.GetPlayerIndex(otherPlayer.ActorNumber));
        PlayerManager = FindLocalPlayerManager();
        PhotonNetwork.DestroyPlayerObjects(otherPlayer);
        UIManager.Instance.UpdateReadyIcons(otherPlayer);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"OnDisconnected() was called by PUN with reason {cause}");

        if (this.CanRecoverFromDisconnect(cause))
        {
            this.Recover();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
        }
    }

    private bool CanRecoverFromDisconnect(DisconnectCause cause)
    {
        switch (cause)
        {
            case DisconnectCause.Exception:
            case DisconnectCause.ServerTimeout:
            case DisconnectCause.ClientTimeout:
            case DisconnectCause.DisconnectByServerLogic:
            case DisconnectCause.DisconnectByServerReasonUnknown:
                return true;
            default:
                return false;
        }
    }

    private void Recover()
    {
        if (!PhotonNetwork.ReconnectAndRejoin())
        {
            Debug.LogError("ReconnectAndRejoin failed, trying Reconnect...");
            if (!PhotonNetwork.Reconnect())
            {
                Debug.LogError("Reconnect failed, trying ConnectUsingSettings...");
                if (!PhotonNetwork.ConnectUsingSettings())
                {
                    Debug.LogError("ConnectUsingSettings failed");
                    // Handle failure to reconnect or connect
                }
            }
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        int startTimestamp;
        bool startTimeIsSet = CountdownTimer.TryGetStartTime(out startTimestamp);
        if (changedProps.ContainsKey(PLAYER_LOADED_LEVEL))
        {
            if (CheckAllPlayerLoadedLevel())
            {
                if (!startTimeIsSet)
                {
                    CountdownTimer.SetStartTime();
                }
            }
            else
            {
                countdownText.text = "Waiting for other players...";
            }
        }
    }

    private bool CheckAllPlayerLoadedLevel()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;

            if (p.CustomProperties.TryGetValue(PLAYER_LOADED_LEVEL, out playerLoadedLevel))
            {
                if ((bool)playerLoadedLevel)
                {
                    continue;
                }
            }

            return false;
        }

        return true;
    }

    private void OnCountdownTimerIsExpired()
    {
        SpawnManager.Instance.StartSpawningEnemies();
    }

    public void LeaveRoom()
    {
        Application.targetFrameRate = 30;
        Debug.Log("[Clicked Quit] Setting FrameRate to 30");
        AudioManager.Instance.StopAllSounds();
        PhotonNetwork.LeaveRoom();
    }

    private PlayerManager FindLocalPlayerManager()
    {
        foreach (PlayerManager playerManager in FindObjectsOfType<PlayerManager>())
        {
            if (playerManager.photonView.IsMine)
            {
                return playerManager;
            }
        }
        return null;
    }

    public void CheckIfAllPlayersReady()
    {
        if (PhotonNetwork.PlayerList.All(player => player.CustomProperties.ContainsKey("IsReady") && (bool)player.CustomProperties["IsReady"]))
        {
            SetGameSpeed(2.0f);
        }
        else
        {
            SetGameSpeed(1.0f);
        }
    }

    private void SetGameSpeed(float speed)
    {
        gameSpeed = speed;
        Time.timeScale = gameSpeed;
    }
}
```csharp

```
</details>

## 2024-10-03
![image](https://github.com/user-attachments/assets/4fc271c5-56a4-4f12-834c-e1d09e432352)

![image](https://github.com/user-attachments/assets/fa6c32dd-d685-4aed-8535-64372398d6ae)

- 데이터 수정용 웹사이트 개발 완료 (Node Express, ReactJs, 웹서버는 nginx를 사용했고 SSL설정 완료 :)! 가상호스트 서버는 그냥 저렴한 Cafe24 월 5500원 주고 결제.)
- JWT 토큰 사용해서 로그인 세션 타임아웃도 설정 완료.

![image](https://github.com/user-attachments/assets/7cd76e3e-7f82-4ae7-88a7-3daf3957c560)

스팀에도 테스트용으로 앱 빌드 및 배포 완료!

## 2024-10-22
https://github.com/user-attachments/assets/1934ae27-693d-472c-832a-c692f4ce4daf

- 게임의 재미 요소를 넣는중...
- 애니메이션 구매 완료. 싹다 바꿔야한다..! 몇백개중에서 고르자니 눈이너무아프다... 좀 더 효율적인 방법 없을까..?


## 번외-첫-포트폴리오-작품

### Unity_Game 개인 게임 작업물 레퍼지토리

![캡처](https://user-images.githubusercontent.com/92070358/146652568-a48bc0a8-3289-41b9-85b8-bd58d5381916.PNG)

![캡처1](https://user-images.githubusercontent.com/92070358/146652573-670c8cdc-2300-4fce-9059-0ce2e686a508.PNG)

![캡처2](https://user-images.githubusercontent.com/92070358/146652575-6bd83ce7-bb5d-43dc-89c4-7d1c3aa06737.PNG)


### 3D RPG 게임
- **작업기간**: 총 5일
- **목표**: 취업 준비 시간 외 여유 시간을 활용하여 퀘스트 + 인벤토리 및 몬스터 추가 작업 -> 포톤을 활용하여 멀티플레이 기능 추가.
- **목표 완수까지 예상 기간**: 2주 내.

#### 설명
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

### 2021-11-25 모바일 전환 및 연동 완료

#### 설명 
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

#### 추가해야할 사항들

1. 보스 구현
2. 전체적 UI 개선
3. 이전 버전의 카메라 Zoom in & out 기능 및 미니맵 재구현.

---

### 2021-12-12 부수적인 기능들 구현완료

1. 보스 구현완료 (보스 점프, 스킬 등 행동 추가 예정)
2. 슬롯 물약 장착기능 완료
3. ETC...

---

### 2021-12-19 부수적인 기능들 구현완료

1. 스킬 및 물리효과 개선 진행중

##### 포트폴리오 동영상 (링크 접속 후 view raw 클릭)

[포트폴리오용 동영상](https://github.com/syoo5953/Unity_Games/blob/main/%EC%9C%A0%EB%8B%88%ED%8B%B0%20%EB%8D%B0%EB%AA%A8%EB%B2%84%EC%A0%84%20%ED%85%8C%EC%8A%A4%ED%8A%B8%EC%9A%A9%20%EA%B2%8C%EC%9E%84%20%ED%94%8C%EB%A0%88%EC%9D%B4%EC%98%81%EC%83%81/(%EB%AA%A8%EB%B0%94%EC%9D%BC%20%EC%A0%84%ED%99%98)%20%ED%8F%AC%ED%8A%B8%ED%8F%B4%EB%A6%AC%EC%98%A4%20%EC%98%81%EC%83%81%20-%203D%20RPG%20%EA%B2%8C%EC%9E%84.mp4)

---

### 2021-12-19 부수적인 기능들 구현완료
1. 라이트 프로빙과 리플렉션 등을 활용한 랜더링 개선
2. MMORPG 다운 게임을 위한 추가적인 기능추가 및 개선 진행중

완성 후 동영상 링크 업로드 예정.

### 2022-01-03 URP로 전환중. UI 개선 및 모바일 최적화 끊임없이 진행중.
[URP 전환중인 동영상](https://github.com/syoo5953/Unity_Games/blob/main/%EC%9C%A0%EB%8B%88%ED%8B%B0%20%EB%8D%B0%EB%AA%A8%EB%B2%84%EC%A0%84%20%ED%85%8C%EC%8A%A4%ED%8A%B8%EC%9A%A9%20%EA%B2%8C%EC%9E%84%20%ED%94%8C%EB%A0%88%EC%9D%B4%EC%98%81%EC%83%81/%EB%AA%A8%EB%B0%94%EC%9D%BC%20RPG%20%EA%B2%8C%EC%9E%84.mp4)

---

### 2022-01-16.
- 적이 사정거리 안에 들어올 시 가까이 돌진하여 공격
- 인벤토리 수정

[본격적인 최적화 & 기능구현 시작 동영상](https://github.com/syoo5953/Unity_Games/blob/main/%EC%9C%A0%EB%8B%88%ED%8B%B0%20%EB%8D%B0%EB%AA%A8%EB%B2%84%EC%A0%84%20%ED%85%8C%EC%8A%A4%ED%8A%B8%EC%9A%A9%20%EA%B2%8C%EC%9E%84%20%ED%94%8C%EB%A0%88%EC%9D%B4%EC%98%81%EC%83%81/(%EB%AA%A8%EB%B0%94%EC%9D%BC%20%26%20URP%20%EC%A0%84%ED%99%98)%20%ED%8F%AC%ED%8A%B8%ED%8F%B4%EB%A6%AC%EC%98%A4%20%EC%98%81%EC%83%81%20-%203D%20RPG%20%EA%B2%8C%EC%9E%84.mp4)

---

### 2022-02-01 쉐이더 학습중
- 쉐이더 그래프를 활용한 카둔랜더링 적용 (Is Front Face의 값을 Flip, 외각선의 Thickness 값을 활용)

![캡처](https://user-images.githubusercontent.com/92070358/151821474-7350168a-cc8d-4463-8fc4-4a341aeb78eb.PNG)

---

### 2022-09-26 기존 RPG 프로젝트 재구축
- 모바일 전환 시 끊김 현상 지속적으로 발생.
- Profiler를 통해 랜더링 및 스크립트에서 부하가 걸린다는 것을 파악.
- 현재까지 쌓인 지식을 토대로 최적화와 개발 동시 진행.
