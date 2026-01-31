# Interaction System - Eymen Arapoğlu

> Ludu Arts Unity Developer Intern Case

## Proje Bilgileri

| Bilgi | Değer |
|-------|-------|
| Unity Versiyonu | 2022.3.62f2 |
| Render Pipeline | URP|
| Case Süresi | 12 saat |
| Tamamlanma Oranı | %29 |

---

## Kurulum

1. Repository'yi klonlayın:
```bash
git clone https://github.com/Eymen179/LuduCase.git
```

2. Unity Hub'da projeyi açın
3. `Assets/[LuduCase]/Scenes/TestScene.unity` sahnesini açın
4. Play tuşuna basın

---

## Nasıl Test Edilir

### Kontroller

| Tuş | Aksiyon |
|-----|---------|
| WASD | Hareket |
| Mouse | Bakış yönü |
| E | Etkileşim |

### Test Senaryoları

1. **Door Test:**
   - Kapıya yaklaşın, "Press E to Open" mesajını görün
   - E'ye basın, kapı açılsın
   - Tekrar basın, kapı kapansın

2. **Key + Locked Door Test:**
   - Kilitli kapıya yaklaşın, "Locked - Key Required" mesajını görün
   - Anahtarı bulun ve toplayın
   - Kilitli kapıya geri dönün, şimdi açılabilir olmalı

3. **Switch Test:**
   - Switch'e yaklaşın ve aktive edin
   - Bağlı nesnenin (kapı/ışık vb.) tetiklendiğini görün

4. **Chest Test:**
   - Sandığa yaklaşın
   - E'ye basılı tutun, progress bar dolsun
   - Sandık açılsın ve içindeki item alınsın

---

## Mimari Kararlar

### Interaction System Yapısı

```
classDiagram
    class InteractionDetector {
        - Camera camera
        - LayerMask interactableLayer
        - IInteractable currentInteractable
        + HandleRaycast()
        + HandleInput()
    }

    class IInteractable {
        <<interface>>
        + OnInteract()
        + OnFocus()
        + OnLoseFocus()
        + InteractionPrompt
    }

    class InteractableBase {
        <<abstract>>
        - Outline outline
        - InteractionType type
        + OnFocus()
        + OnLoseFocus()
    }

    class Door {
        + OnInteract()
    }
    class Chest {
        + OnInteract()
    }

    InteractionDetector --> IInteractable : Uses
    InteractableBase ..|> IInteractable : Implements
    Door --|> InteractableBase : Inherits
    Chest --|> InteractableBase : Inherits
```

**Neden bu yapıyı seçtim:**
> Bu mimariyi seçmemin temel nedeni Modülerlik ve Genişletilebilirliktir.

Decoupling (Bağımsızlaştırma): InteractionDetector (Oyuncu) etkileşime girdiği nesnenin bir Kapı mı yoksa Sandık mı olduğunu bilmez; sadece IInteractable arayüzünü tanıdığı için kod karmaşası yaşanmaz.

Kod Tekrarını Önleme: InteractableBase sınıfı sayesinde Outline (dış hat çizimi), zamanlayıcılar ve temel tanımlamalar tek bir merkezden yönetilir.

New Input System Entegrasyonu: Unity'nin modern event-driven input sistemi ile birleştirerek, gelecekte Gamepad veya diğer kontrolcülerle kolayca çalışabilir hale getirdim.

**Alternatifler:**
> 1. Trigger Collider Yaklaşımı (OnTriggerEnter):

Neden Seçilmedi: FPS/TPS oyunlarında oyuncunun "baktığı" nesne ile etkileşime girmesi, "yanında durduğu" nesne ile etkileşime girmesinden daha doğaldır. Raycast, hassas seçim (örn: masadaki 5 anahtardan sadece birini almak) imkanı sunar.

2. Monolitik Player Controller:

Neden Seçilmedi: if (hit.tag == "Door") { ... } else if (hit.tag == "Chest") gibi iç içe if yapıları kullanmak, her yeni eşya eklendiğinde Player kodunu değiştirmeyi gerektirir. Bu da "Open/Closed Principle"a aykırıdır ve bakımı zordur.

**Trade-off'lar:**
> Avantajlar:

Yeni bir etkileşimli nesne eklemek için Player koduna dokunmaya gerek yoktur; sadece InteractableBase'den türetmek yeterlidir.

Görsel geri bildirim (Outline) ve Input mantığı birbirinden ayrılmıştır.

Dezavantajlar / Zorluklar:

Hassasiyet Gereksinimi: Raycast tabanlı sistemlerde oyuncunun tam olarak objenin üzerine (collider'ına) bakması gerekir. Küçük objelerde bu durum oyuncuyu zorlayabilir (Çözüm: SphereCast kullanımı düşünülebilir).

Kurulum Maliyeti: Her yeni objenin doğru Layer (Interactable) ve Collider ayarlarının yapılmış olması zorunludur, aksi takdirde sistem nesneyi görmezden gelir.

### Kullanılan Design Patterns

| Pattern | Kullanım Yeri | Neden |
|---------|---------------|-------|
| [Observer] | [Event system] | Daha Verimşi |
| [State] | [Door states] | Daha Verimli|

---

## Ludu Arts Standartlarına Uyum

### C# Coding Conventions

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| m_ prefix (private fields) | [x] / [x] | |
| s_ prefix (private static) | [x] / [x] | |
| k_ prefix (private const) | [x] / [x] | |
| Region kullanımı | [x] / [x] | |
| Region sırası doğru | [x] / [x] | |
| XML documentation | [x] / [x] | |
| Silent bypass yok | [x] / [x] | |
| Explicit interface impl. | [x] / [ ] | |

### Naming Convention

| Kural | Uygulandı | Örnekler |
|-------|-----------|----------|
| P_ prefix (Prefab) | [x] / [x] | P_Door, P_Chest |
| M_ prefix (Material) | [x] / [x] | M_Door_Wood |
| T_ prefix (Texture) | [x] / [x] | |
| SO isimlendirme | [x] / [x] | |

### Prefab Kuralları

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| Transform (0,0,0) | [x] / [x] | |
| Pivot bottom-center | [x] / [x] | |
| Collider tercihi | [x] / [x] | Box > Capsule > Mesh |
| Hierarchy yapısı | [x] / [x] | |

### Zorlandığım Noktalar
> Projenin genel olarak algoritma yapısını hazırlamak zor oldu.
---

## Tamamlanan Özellikler

### Zorunlu (Must Have)

- [x] / [x] Core Interaction System
  - [x] / [x] IInteractable interface
  - [x] / [x] InteractionDetector
  - [x] / [x] Range kontrolü

- [x] / [x] Interaction Types
  - [x] / [] Instant
  - [x] / [ ] Hold
  - [x] / [x] Toggle

- [x] / [x] Interactable Objects
  - [x] / [x] Door (locked/unlocked)
  - [x] / [ ] Key Pickup
  - [x] / [ ] Switch/Lever
  - [x] / [ ] Chest/Container

- [x] / [x] UI Feedback
  - [x] / [x] Interaction prompt
  - [x] / [x] Dynamic text
  - [x] / [ ] Hold progress bar
  - [x] / [ ] Cannot interact feedback

- [x] / [x] Simple Inventory
  - [x] / [ ] Key toplama
  - [x] / [ ] UI listesi

### Bonus (Nice to Have)

- [ ] Animation entegrasyonu
- [ ] Sound effects
- [ ] Multiple keys / color-coded
- [x] Interaction highlight
- [ ] Save/Load states
- [ ] Chained interactions

---

## Bilinen Limitasyonlar

### Tamamlanamayan Özellikler
1. Key sistemi - Algoritma kurulamadı.
2. Lever - Algoritma kurulamadı.

### Bilinen Bug'lar
1. Kapının toggle sistemi hızlı hızlı basınca çalışmamaya başladı.

---

## Dosya Yapısı

```
Assets/
├── [ProjectName]/
│   ├── Scripts/
│   │   ├── Runtime/
│   │   │   ├── Core/
│   │   │   │   ├── IInteractable.cs
│   │   │   │   └── ...
│   │   │   ├── Interactables/
│   │   │   │   ├── Door.cs
│   │   │   │   └── ...
│   │   │   ├── Player/
│   │   │   │   └── ...
│   │   │   └── UI/
│   │   │       └── ...
│   │   └── Editor/
│   ├── ScriptableObjects/
│   ├── Prefabs/
│   ├── Materials/
│   └── Scenes/
│       └── TestScene.unity
├── Docs/
│   ├── CSharp_Coding_Conventions.md
│   ├── Naming_Convention_Kilavuzu.md
│   └── Prefab_Asset_Kurallari.md
├── README.md
├── PROMPTS.md
└── .gitignore
```

---

## İletişim

| Bilgi | Değer |
|-------|-------|
| Ad Soyad | Eymen Arapoğlu |
| E-posta | eymenarapoglu5454@gmail.com |
| LinkedIn | https://www.linkedin.com/in/eymen-arapo%C4%9Flu-3543a8262/ |
| GitHub | github.com/Eymen179 |

---
