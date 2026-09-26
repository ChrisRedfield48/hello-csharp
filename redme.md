# hello-csharp — C#/.NET CI/CD с публикацией в GitHub Releases

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![CI](https://img.shields.io/badge/CI-GitHub%20Actions-2088FF?logo=githubactions&logoColor=white)
![Release](https://img.shields.io/badge/Release-Single--File%20Binary-informational)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

Учебный проект на **C#/.NET**, демонстрирующий настройку CI/CD через **GitHub Actions**: автоматические тесты на каждый push, и публикация self-contained-бинарников под три платформы в **GitHub Releases** по тегу версии.

## Содержание

- [Описание проекта](#описание-проекта)
- [Структура проекта](#структура-проекта)
- [Требования](#требования)
- [Пайплайн CI/CD](#пайплайн-cicd)
- [Локальная разработка](#локальная-разработка)
- [Создание релиза](#создание-релиза)
- [Скачивание и запуск бинарника](#скачивание-и-запуск-бинарника)
- [Лицензия](#лицензия)

## Описание проекта

Проект состоит из библиотеки `Greeting` с простой логикой приветствия, консольного приложения `App`, которое её использует, и набора unit-тестов `Greeting.Tests`. На каждый push и pull request GitHub Actions прогоняет сборку и тесты. При создании тега `v*` дополнительно запускается публикация self-contained однофайловых бинарников под Linux, macOS (Apple Silicon) и Windows прямо в GitHub Releases.

## Структура проекта

```
hello-csharp/
├── .github/
│   └── workflows/
│       └── ci.yml               # Конфигурация пайплайна
├── src/
│   ├── App/
│   │   └── App.csproj            # Консольное приложение
│   └── Greeting/
│       └── Greeting.csproj       # Библиотека с логикой приветствия
├── tests/
│   └── Greeting.Tests/
│       └── Greeting.Tests.csproj # Unit-тесты (xUnit)
├── hello-csharp.slnx              # Solution-файл
└── README.md
```

## Требования

- .NET SDK 8.0+ (для сборки/запуска, локально можно использовать более новый SDK — он умеет собирать под старые `TargetFramework`)
- Docker (опционально, для контейнеризованной сборки)

## Пайплайн CI/CD

Пайплайн настроен в [`​.github/workflows/ci.yml`](.github/workflows/ci.yml) и состоит из двух джобов:

| Job | Когда запускается | Что делает |
|-----|--------------------|-----------|
| **test** | push в `main`, pull request | `dotnet restore` → `dotnet build` → `dotnet test` |
| **release** | push тега `v*` | Матричная сборка на 3 конфигурациях (`linux-x64`, `osx-arm64`, `win-x64`), публикация self-contained однофайловых бинарников через `dotnet publish` и загрузка их в GitHub Release |

Job `release` зависит от успешного прохождения `test` (`needs: test`) — релиз не соберётся, пока не пройдут тесты.

## Локальная разработка

Восстановление зависимостей:

```bash
dotnet restore
```

Сборка:

```bash
dotnet build
```

Запуск приложения:

```bash
dotnet run --project src/App/App.csproj
```

Запуск тестов:

```bash
dotnet test
```

## Создание релиза

Когда код в `main` стабилен — создай тег версии и запушь его:

```bash
git tag v1.0.0
git push origin v1.0.0
```

Это запустит job `release`, который соберёт бинарники параллельно на трёх платформах и создаст **GitHub Release** с прикреплёнными файлами:

- `hello-csharp-linux-x64`
- `hello-csharp-macos-arm64`
- `hello-csharp-windows-x64.exe`

## Скачивание и запуск бинарника

Linux:

```bash
wget https://github.com/ChrisRedfield48/hello-csharp/releases/download/v1.0.0/hello-csharp-linux-x64
chmod +x hello-csharp-linux-x64
./hello-csharp-linux-x64
```

macOS (Apple Silicon):

```bash
curl -L -o hello-csharp-macos-arm64 \
  https://github.com/ChrisRedfield48/hello-csharp/releases/download/v1.0.0/hello-csharp-macos-arm64
chmod +x hello-csharp-macos-arm64
./hello-csharp-macos-arm64
```

Windows (PowerShell):

```powershell
Invoke-WebRequest `
  -Uri "https://github.com/ChrisRedfield48/hello-csharp/releases/download/v1.0.0/hello-csharp-windows-x64.exe" `
  -OutFile "hello-csharp.exe"

.\hello-csharp.exe
```

> На macOS при первом запуске может появиться предупреждение безопасности — снимается через **Системные настройки → Приватность и безопасность → Всё равно открыть**.

## Лицензия

Проект распространяется под лицензией MIT — см. файл [LICENSE](LICENSE).