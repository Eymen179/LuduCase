# LLM Kullanım Dokümantasyonu

## Özet

| Bilgi | Değer |
|-------|-------|
| Toplam prompt sayısı | X |
| Kullanılan araçlar | Gemini |
| En çok yardım alınan konular | Unity - Algoritma - C# |
| Tahmini LLM ile kazanılan süre | 2 saat |

---

## Prompt 1: Unity New Input System

**Araç:** Gemini
**Tarih/Saat:** 2026 - 01 - 31 14.31

**Prompt:**
```
Unity'de yeni Input sistemine geçişin nasıl olduğunu anlat ve temel olarak wasd ve mouse sol tıkı için yeni input girişi oluştur.
```

**Alınan Cevap (Özet):**
```
Unity'nin New Input System (Yeni Giriş Sistemi), eski Input.GetAxis veya Input.GetKey yöntemlerine göre çok daha modüler, event (olay) tabanlı ve çoklu platform (klavye, gamepad, dokunmatik ekran) desteği sunan güçlü bir yapıdır.

Aşağıda bu sisteme geçişi, kurulumu ve istediğin WASD + Sol Tık yapısını nasıl kuracağını adım adım anlattım.

1. Kurulum ve Etkinleştirme
Eski sistemi yenisiyle değiştirmek için önce paketi yüklemeliyiz.

Unity'de Window > Package Manager yolunu izle.

Sol üstten Unity Registry'yi seç.

Listeden Input System paketini bul ve Install butonuna bas.

Önemli Uyarı: Yükleme sırasında Unity sana "Backend'i değiştirmek için editörü yeniden başlatmam gerekiyor, onaylıyor musun?" diye soracak. Yes diyerek editörün yeniden başlamasına izin ver.

Not: Projen yeniden açıldığında eski Input komutların çalışmayabilir. Hem eski hem yeniyi aynı anda kullanmak istersen: Project Settings > Player > Other Settings > Active Input Handling kısmını Both yapabilirsin.

2. Input Actions Asset'ini Oluşturma
Kod yazmadan önce tuş atamalarını yapacağımız bir harita (Asset) oluşturmalıyız.

Project panelinde boş bir yere sağ tıkla: Create > Input Actions.

Dosyaya bir isim ver (örneğin: GameControls).

Oluşturduğun bu dosyaya çift tıkla, editör penceresi açılacak.

A. Action Map Oluşturma
Sol sütunda Action Maps yanındaki (+) butonuna bas.

İsim olarak Player yaz.

B. WASD Hareketi (Move) Tanımlama
Ortadaki Actions sütununda New action yazan yere gel ve ismini Move yap.

Sağdaki Action Properties panelinde:

Action Type: Value seç.

Control Type: Vector 2 seç (Çünkü X ve Y düzleminde hareket edeceğiz).

Move aksiyonunun yanındaki küçük (+) butonuna bas ve Add Up\Down\Left\Right Composite seçeneğini seç.

Şimdi altına WASD tuşlarını atayacağız:

Up: Klavyeden W [Keyboard].

Down: Klavyeden S [Keyboard].

Left: Klavyeden A [Keyboard].

Right: Klavyeden D [Keyboard].

C. Mouse Sol Tık (Fire) Tanımlama
Actions yanındaki (+) butonuna basarak yeni bir aksiyon oluştur. İsmini Fire yap.

Action Properties panelinde:

Action Type: Button seç.

Fire altındaki <No Binding> kısmına tıkla.

Path kısmına gel ve Left Button [Mouse] seçeneğini bulup seç.

Son Adım: Pencerenin üstündeki Save Asset butonuna basmayı unutma!
```

**Nasıl Kullandım:**
- [ ] Direkt kullandım (değişiklik yapmadan)
- [x] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Bu bilgiyi unutmuştum hatırlamak için sordum.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> İsimlendirmeleri değiştirdim.

---

## Prompt 2: [İstenilenler]

**Araç:** Gemini
**Tarih/Saat:** 2026 - 01 - 31 15.20

**Prompt:**
```
Yapması istenilen içeriği md dosyalarından kopyalayıp Gemini'ye attım. Bunlara uygun kodları yazmasını istedim.
```

**Alınan Cevap (Özet):**
```
Bana kodları verdi.
```

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

---

## Genel Değerlendirme

### LLM'in En Çok Yardımcı Olduğu Alanlar
1. Unity
2. C#
3. Algoritma

### LLM'in Yetersiz Kaldığı Alanlar
1. Algoritma

### LLM Kullanımı Hakkında Düşüncelerim
> Bilmediğim algoritma yapılarını öğrendim. Gittikçe daha da efektif kullanmaya başladım.

---

