# Chess Hub ♟️

**Online real-time chess with automatic opponent matching**

[![Unity](https://img.shields.io/badge/Unity-6black?style=flat-square&logo=unity)](https://unity.com/)
[![Photon PUN2](https://img.shields.io/badge/Photon-PUN2-blue?style=flat-square)](https://www.photonengine.com/pun)
[![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20WebGL-green?style=flat-square)](https://github.com/your-username/chess-duel-online)

## 🎮 Description

Chess Hub is a multiplayer chess game with automatic opponent matching. Create a game room, find an opponent, and play classic chess according to FIDE rules.

## ✨ Features

- 🌐 **Online PvP** — play against real players
- 🎯 **Automatic matchmaking** — quickly find an opponent
- ♟️ **Classic rules** — full FIDE rules implementation
- 🏆 **Simple interface** — intuitive UI
- 🎨 **Minimalist design** — focus on gameplay

## 🎯 Gameplay

### Game Mode
- **Type:** Turn-based strategy / PvP
- **Players:** 2 players (1v1)
- **Time:** No limits (so far)

### Rules
- Standard 8×8 board
- All pieces: Pawn, Knight, Bishop, Rook, Queen, King
- Movement by FIDE rules
- Win by checkmate or resignation

### UI Elements
- Turn indicator (white/black)
- Opponent's name
- "Resign" button
- "Restart" button

## 🔧 Technical Details

### Technology Stack
- **Engine:** Unity 6 URP
- **Networking:** Photon PUN2
- **Language:** C#
- **Architecture:** DDD with MVVM
- **Data:** Scriptable Objects

## 🎮 How to Play

1. **Launch the game** — the main menu will open
2. **Click "Play"** — matchmaking will start
3. **Wait for connection** — Player A gets the white pieces
4. **Make your moves** — click on a piece and select a square
5. **Win** — checkmate your opponent or wait for their resignation

## 🎨 Visual Style

- **Board:** Simple 3D model with cells
- **Pieces:** Minimalist cubes (temporary)
- **Colors:** Classic white/black
- **UI:** Clean, functional design

## 📱 Supported Platforms

- ✅ **Windows** (main)
- ✅ **WebGL** (browser)
- 🔄 **Android** (planned)

## 🔮 Roadmap

### Near-Term Goals
- [ ] Timer for blitz games
- [ ] ELO rating system
- [ ] Lobby for selecting an opponent
- [ ] "Play with Friend" mode

### Long-Term Goals
- [ ] 3D models of pieces
- [ ] Move animations
- [ ] Skins system
- [ ] Replay saving and playback
- [ ] Mobile version

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push your branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📋 Known Issues

- No reconnection in case of disconnection
- Move validation is absent on the server
- UI is not responsive for mobile devices

## 📄 License

This project is licensed under the MIT License - see [LICENSE](LICENSE) file for details.

## 🎯 Contact

**Developer:** Daniel Sarabuna
- GitHub: [danielsarabuna](https://github.com/danielsarabuna)
- Email: danielsarabuna@gmail.com

---

⭐ **Consider giving a star if you liked the project!**