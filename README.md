# copilot-journey
Sample repository demonstrating several features of GitHub Copilot.

## Overview
In this sample repository you'll find work demonstrating various GitHub Copilot features.
The sample is an API built on the .NET stack. Examples include:
- Documentation generation with autocompletion
- Project initialization
- Using instructions to guide Copilot
- Using chat modes

## Sample API
The sample API is a simple Weather Forecast API that exposes endpoints to retrieve weather data:
- Current weather data
- 7-day weather forecast
- Weather alerts

The weather data is retrieved from an open-source API: https://open-meteo.com/.

The API is built using the ASP.NET Core Web API framework.

The project structure initially follows Copilot's natural suggestions and is then adjusted to follow best practices for maintainability and scalability. This README should not overly influence suggestions.

We'll create the initial commits manually, then ask Copilot to generate commit messages for subsequent commits.

## Repository instruction files

Note: these instruction and chat-mode files are derived from and inspired by the examples in the GitHub repository "awesome-copilot" — https://github.com/github/awesome-copilot/tree/main/. They have been adapted for this sample project and live under `./.github`.

### Instruction files
This repository includes a set of guideline files under `.github/instructions` that are used to steer code generation and reviews. They are human-readable guidance files (not executable) and help ensure consistent style, security, and architectural practices when using AI-assisted tools like Copilot.

The instruction files in this project are:

- `./.github/instructions/aspnet-rest-apis.instructions.md` — Guidelines for building REST APIs with ASP.NET (applies to `**/*.cs`, `**/*.json`).
- `./.github/instructions/csharp.instructions.md` — General guidelines for building C# applications (applies to `**/*.cs`).
- `./.github/instructions/dotnet-architecture-good-practices.instructions.md` — DDD and .NET architecture best-practices (applies to `**/*.cs`, `**/*.csproj`, `**/Program.cs`, `**/*.razor`).
- `./.github/instructions/markdown.instructions.md` — Documentation and content creation standards for Markdown files (applies to `**/*.md`).
- `./.github/instructions/object-calisthenics.instructions.md` — Object Calisthenics principles to promote clean domain code (applies to `**/*.{cs,ts,java}`).
- `./.github/instructions/security-and-owasp.instructions.md` — Comprehensive secure-coding and OWASP-based guidelines (applies to all files).

- `./.github/instructions/clean-architecture.instructions.md` — Clean Architecture principles and project structure for .NET solutions (applies to `**/*.cs`).
- `./.github/instructions/coding-style-csharp.instructions.md` — C# coding style and conventions (applies to `**/*.cs`).
- `./.github/instructions/conventional-commits.instructions.md` — Conventional Commits specification and examples (applies to `*`).
- `./.github/instructions/domain-driven-design.instructions.md` — Domain-Driven Design (DDD) modeling guidelines, aggregates, value objects, and repositories (applies to `**`).
- `./.github/instructions/follow-up-question.instructions.md` — Repository process rule that requires confidence percentages and follow-up questions for ambiguous tasks (applies to `**`).
- `./.github/instructions/unit-and-integration-tests.instructions.md` — Unit and integration testing rules, recommended tools (xUnit v3, FakeItEasy, Testcontainers) and placement (applies to `**/*.cs`).

How to use instructions

- Open the files under `./.github/instructions` to read the guidance. They describe conventions, security requirements, and formatting rules that contributors (and Copilot) should follow.
- When using Copilot or reviewing AI-generated code, reference these documents to enforce secure defaults (for example: no hardcoded secrets, parameterized queries, output encoding for XSS, and recommended algorithms).

If you add or change any instruction file, commit the edits so Copilot and other tooling can pick up the updated guidance.

### Prompt files

This repository also contains a set of prompts under `./.github/prompts`. Each `.md` file provides prompt templates and guidance for specific tasks. 

 - `./.github/prompts/conventional-commit.prompt.md` — A prompt and guidance for generating Conventional Commit-style messages (used when composing commit messages or when asking Copilot to suggest commit text).

How to use a prompt

- In Copilot chat, enter `/<prompt_name>` (for example, `/conventional-commit`).
- Provide any additional context the prompt asks for (files changed, PR description, intended scope). The prompt will guide the assistant to produce an output that follows the repository's conventions.
- Optionally, adapt the prompt and save a copy in this folder if you want a project-specific variant.

### Chat modes

This repository also contains a set of chat-mode workflows under `./.github/chatmodes`. Each `.md` file encodes a small, opinionated process for an interactive development phase (useful when running Copilot Chat or following a TDD cycle interactively).

Files included:

- `./.github/chatmodes/tdd-red.chatmode.md` — the RED phase: guidance for writing the simplest failing test first based on the linked GitHub issue. It emphasises test-first, small focused tests, and pulling requirements from issue context.
- `./.github/chatmodes/tdd-green.chatmode.md` — the GREEN phase: instructions for implementing the minimal code to make the failing test pass quickly (hard-coded values or simple conditionals are acceptable until refactor).
- `./.github/chatmodes/tdd-refactor.chatmode.md` — the REFACTOR phase: guidance for improving design, applying security best-practices, and cleaning up while keeping tests green.

How to use chat modes
- In the list of Copilot Chat modes, select the required mode (for example, "TDD Red Phase - Write Failing Tests First").
- Submit your request in Copilot discussion window. 

These chat-mode workflows are designed to be practical, human-friendly guides to help you run consistent TDD iterations with assistance from Copilot or other chat-based tools.
