using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMG_Zombies.Scripts.Managers
{
    public class KeyboardManager
    {
        #region Campos e Propriedes

        private KeyboardManager keyboardManager;
        private Dictionary<Keys, KeyState> keysAndState;

        private enum KeyState
        {
            PRESSED,
            HELD,
            UP,
            NONE
        }

        #endregion


        #region carregar teclado do jogo

        public KeyboardManager()
        {
            if (keyboardManager == null)
            {
                keysAndState = new Dictionary<Keys, KeyState>();
                keyboardManager = this;
            }
            else
            {
                throw new Exception("Erro: Uma instância já foi criada!");
            }
        }

        #endregion


        #region Atualizar teclado do jogo

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            Keys[] pressedKeys = state.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (!keysAndState.ContainsKey(key))
                {
                    keysAndState.Add(key, KeyState.PRESSED);
                }
                else
                {
                    if (keysAndState[key] == KeyState.PRESSED)
                    {
                        keysAndState[key] = KeyState.HELD;
                    }
                    else if (keysAndState[key] == KeyState.UP || keysAndState[key] == KeyState.NONE)
                    {
                        keysAndState[key] = KeyState.PRESSED;
                    }
                }
            }

            foreach (Keys key in keysAndState.Keys.ToArray())
            {
                if (!pressedKeys.Contains(key))
                {
                    if (keysAndState[key] == KeyState.UP)
                    {
                        keysAndState[key] = KeyState.NONE;
                    }
                    else if (keysAndState[key] == KeyState.PRESSED || keysAndState[key] == KeyState.HELD)
                    {
                        keysAndState[key] = KeyState.UP;
                    }
                }
            }
        }

        public bool IsKeyPressed(Keys key) => keysAndState.ContainsKey(key) && keysAndState[key] == KeyState.PRESSED;

        public bool IsKeyUp(Keys key) => keysAndState.ContainsKey(key) && keysAndState[key] == KeyState.UP;

        public bool isKeyHeld(Keys key) => keysAndState.ContainsKey(key) && keysAndState[key] == KeyState.HELD;

        #endregion
    }
}