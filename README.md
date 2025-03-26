# 🏰 RPG Adventure - Unity 게임 프로젝트 

<div align="center">
  <img src="https://via.placeholder.com/800x400" alt="게임 스크린샷" width="70%">  
  (※ 실제 스크린샷으로 대체)
</div>

## 📜 프로젝트 개요
- **장르**: 액션 RPG  
- **개발 기간**: 2024.09 ~ 2024.12  
- **주요 기술**: `Unity` `C#`  
- **목표**: 장비 성장과 퀘스트 시스템을 통해 플레이어의 지속적인 성취감 유도  

---

## 🎮 주요 기능

### 1. 장비 시스템
- **빛기통 상호작용**으로 초보자 장비 획득  
- 인벤토리 내 **희귀도/부위별 정렬**  
```csharp
// 예시 코드 (인벤토리 정렬)
items.Sort((a, b) => a.Rarity.CompareTo(b.Rarity));

### 2. 퀘스트 시스템
- NPC 대화를 통한 퀘스트 수락/완료
- 보상으로 포션 지급

### 3. 몬스터 전투
- 몬스터 사망 시 랜덤 장비 드롭
- 스킬 쿨타임 UI 연동

```markdown
   ![Unity Version](https://img.shields.io/badge/Unity-2022.3+-black?logo=unity)
   ![License](https://img.shields.io/badge/License-MIT-green)
