# ⚔️ JRPG Battle System – Unity 3D

Um sistema de batalha por turnos no estilo JRPG clássico, inspirado em Final Fantasy VII, desenvolvido com Unity 3D.  
O projeto inclui exploração de mundo, encontros aleatórios, sistema de turnos com barra de progresso (ATB), magias, seleção de inimigos e gerenciamento de estado entre cenas.

<!--![Gameplay Screenshot](https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/RPG-Turn-FF/main/Screens/Captura%20de%20tela%202026-03-20%20000009-.png)-->

---

## 🎮 Funcionalidades

- ✅ Explorar mapas com detecção de zonas de encontro
- ✅ Sistema de batalha por turnos com fila de ações
- ✅ Barra de progresso (ATB) para heróis e inimigos
- ✅ Seleção de ataques físicos e mágicos
- ✅ Interface com painéis de status atualizáveis
- ✅ Troca entre cenas (mundo → batalha → mundo)
- ✅ Pontos de teletransporte e spawn
- ✅ Gerenciamento global de estado com `GameManager`

---

## 🧠 Estrutura Técnica

- `GameManager`: singleton persistente entre cenas
- `BattleStateMachine`: controla o fluxo da batalha (turnos, ações, vitória/derrota)
- `HeroStateMachine` / `EnemyStateMachine`: lógica individual de cada combatente
- `BaseHero`, `BaseEnemy`, `BaseAttack`: classes serializáveis para dados
- Sistema de turnos com `HandleTurn` e lista de ações pendentes

---

## 📸 Prévia

<!--![Gameplay Screenshot]
<img src="https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/RPG-Turn-FF/main/Screens/Captura%20de%20tela%202026-03-20%20000009-.png" width="600" alt="Gameplay Screenshot">-->

<!--[<img src="https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/RPG-Turn-FF/main/Screens/Captura%20de%20tela%202026-03-20%20000009-.png" width="600" alt="Gameplay Screenshot">](<img src="https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/RPG-Turn-FF/main/Screens/Captura%20de%20tela%202026-03-20%20000009-.png" width="600" alt="Gameplay Screenshot">)]-->
<!--<a href="https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/RPG-Turn-FF/main/Screens/Captura%20de%20tela%202026-03-20%20000009-.png">
    <img src="https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/RPG-Turn-FF/main/Screens/Captura%20de%20tela%202026-03-20%20000009-.png" 
         width="400" 
         height="400" 
         alt="Gameplay Screenshot">
</a>-->
<!--<div align="center">
  <img src="https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/RPG-Turn-FF/main/Screens/Captura%20de%20tela%202026-03-20%20000009-.png" width="400" height="400" alt="Gameplay Screenshot">
</div>
> *Para ampliar Imagem da gameplay demonstrando o sistema de batalha*
-->
<div align="center">
  <img src="https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/RPG-Turn-FF/main/Screens/Captura%20de%20tela%202026-03-20%20000009-.png" width="400" height="400" alt="Gameplay Screenshot">
    <div align="center">
    > *Para ampliar Imagem da gameplay demonstrando o sistema de batalha*
    </div>
</div>

## 🎥 Gameplay

<!--[![Gameplay Preview](COLE_AQUI_O_LINK_RAW_DA_IMAGEM)](COLE_AQUI_O_LINK_DO_VIDEO_NO_YOUTUBE)-->

[<img src="https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/RPG-Turn-FF/main/Screens/Captura%20de%20tela%202026-03-20%20000009-.png" width="600" alt="Gameplay Screenshot">](https://vimeo.com/1182761267)


*Clique na imagem para assistir ao vídeo de gameplay*


## 🛠️ Como testar

1. Clone o repositório
2. Abra o projeto no Unity 6.3 LTS (6000.3.8f1 ou superior)
3. Abra a cena principal
4. Pressione Play e explore o mapa
5. Entre em zonas de encontro (tag `EncouterZone`) para iniciar batalhas

---

## 📦 Pré-requisitos

- Unity 6.3 LTS (6000.3.8f1)
- Conhecimento básico de C# e máquinas de estado

---

## 📄 Licença

Este projeto é de uso livre para estudos e aprendizado.

---

## 🙌 Créditos
OCTOMAN GAMES

Desenvolvido como parte de um estudo prático sobre RPGs por turnos em Unity.
