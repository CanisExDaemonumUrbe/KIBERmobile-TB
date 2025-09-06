# KIBERmobile

Кроссплатформенное мобильное приложение на базе **.NET MAUI**, разработанное для iOS и Android.  
Проект объединяет общий код и специфические реализации для каждой платформы.  

---

## О проекте
**KIBERmobile** — это мобильная версия **личного кабинета резидента образовательной франшизы**.  
Приложение предоставляет удобный доступ к функционалу личного кабинета прямо со смартфона:

- Лента новостей и обновлений  
- Раздел KIBERshop (внутренний магазин)  
- Просмотр и редактирование личных данных  
- Управление подписками и услугами  
- Центр поддержки и обратная связь  

Проект лёг в основу реального внутреннего продукта образовательной франшизы и продолжает развиваться.  

---

## Стек технологий

### Общий проект
- **Платформа:** .NET 9.0  
- **Фреймворк:** .NET MAUI  
- **UI-библиотека:** Microsoft.Maui.Controls  
- **Дополнительные пакеты:**
  - CommunityToolkit.Maui `9.1.0`  
  - Microsoft.Extensions.Logging.Debug `9.0.0-rc.2.24473.5`  
  - Newtonsoft.Json `13.0.3`  
- **Особенности:**
  - Поддержка XAML-страниц и CodeBehind  
  - Ресурсы: изображения, шрифты, raw-ассеты  
  - SplashScreen (iOS + Android)  
  - Сборки: `Debug`, `Release`, `Distribution`  

---

### iOS
- **Target:** net9.0-ios  
- **Минимальная версия iOS:** 14.0  
- **Выходной тип:** Exe  
- **Сборки:** Debug / Release / Distribution  
- **Особенности:**
  - Ahead-of-Time (AOT) компиляция  
  - Используется LLVM  
  - Bitcode отключён  
  - Поддержка SGen GC (concurrent mode)  
  - Линковка: `SdkOnly` (Release/Distribution), `None` (Debug)  
  - Настроена подпись кода (Development / Distribution)  

---

### Android
- **Target:** net9.0-android  
- **Минимальная версия Android:** API 21  
- **Выходной тип:** Exe  
- **Сборки:** Debug / Release  
- **Особенности:**
  - ABI: `armeabi-v7a`, `arm64-v8a`, `x86`, `x86_64`  
  - Release:
    - Linker: `r8`  
    - Формат: `.aab` (Android App Bundle)  
    - Отключены отладочные символы  
    - Предпочтение 32-битной сборки  
  - Debug:
    - Без линковки (`LinkMode=None`)  
    - Shared Runtime включён  
- **Ресурсы:**
  - Поддержка разных плотностей экранов: `hdpi`, `mdpi`, `xhdpi`, `xxhdpi`, `xxxhdpi`  

---

## Запуск проекта

### 1. Клонирование репозитория
```bash
git clone https://github.com/<username>/KIBERmobile.git
cd KIBERmobile
```

### 2. Запуск
- **Android:** запуск через Android Emulator или физическое устройство  
- **iOS:** запуск через Xcode / iOS Simulator (требуется macOS)  
- **Общий проект:** стандартный запуск через `dotnet build` / `dotnet run`  

---

## Структура проекта

Репозиторий разделён на три части: общий код и платформенные проекты под Android и iOS.

```
KIBERmobile/                  # Общий проект (бизнес-логика и UI)
 ├── Controllers/             # Контроллеры и логика взаимодействия
 ├── HttpApi/                 # API-клиенты и сетевые запросы
 ├── LocalPackages/           # Локальные пакеты и библиотеки
 ├── Models/                  # Модели данных
 ├── Resources/               # Общие ресурсы (шрифты, изображения и др.)
 ├── ViewModels/              # ViewModel-слой (MVVM)
 ├── Views/                   # XAML-страницы и код за ними
 ├── App.xaml                 # Конфигурация приложения
 ├── AppTabbedPage.xaml       # Навигация через вкладки
 └── KIBERmobile.csproj       # Конфигурация общего проекта

KIBERmobile.Droid/            # Android-часть
 ├── Assets/                  # Android-активы
 ├── Resources/               # Android-ресурсы (drawable и др.)
 ├── AndroidManifest.xml      # Манифест приложения
 ├── MainActivity.cs          # Точка входа для Android
 ├── MainApplication.cs       # Класс приложения
 └── KIBERmobile.Droid.csproj # Конфигурация Android-проекта

KIBERmobile.iOS/              # iOS-часть
 ├── Assets.xcassets/         # Ресурсы iOS (иконки, изображения)
 ├── Resources/               # Ресурсы iOS
 ├── AppDelegate.cs           # Делегат приложения
 ├── Info.plist               # Конфигурация приложения iOS
 ├── Entitlements.plist       # Права и capabilities
 ├── Main.cs                  # Точка входа для iOS
 └── KIBERmobile.iOS.csproj   # Конфигурация iOS-проекта
```

---

## Автор
- Danil Gelimyanov

---

## Лицензия
Код проекта принадлежит автору и используется в рамках внутреннего продукта образовательной франшизы.  
Использовать исходный код без разрешения автора **запрещается**.  
