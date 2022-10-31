using MSFD;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class AfterMatchController : NetworkBehaviour
    {
        [SerializeField]
        float delayAfterMatchEnd = 3;

        public void EndGame(PlayerEnum wonPlayer)
        {
            bool isWonFirstPlayer = wonPlayer == PlayerEnum.playerOne;
            DisplayEndMessageClientRPC(isWonFirstPlayer);
            StartCoroutine(AfterMatchEnd());
        }
        [ClientRpc]
        void DisplayEndMessageClientRPC(bool isWonFirstPlayer)
        {
            if (!IsHost)
            {
                if (isWonFirstPlayer)
                    DisplayLooseMessage();
                else
                    DisplayWinMessage();
            }
            else
            {
                if (!isWonFirstPlayer)
                    DisplayLooseMessage();
                else
                    DisplayWinMessage();
            }
        }

        void DisplayWinMessage()
        {
            Messenger<string>.Broadcast(GameEventsPong.R_string_DISPLAY_MESSAGE, "You Win!", MessengerMode.DONT_REQUIRE_LISTENER);
        }
        void DisplayLooseMessage()
        {
            Messenger<string>.Broadcast(GameEventsPong.R_string_DISPLAY_MESSAGE, "You Loose", MessengerMode.DONT_REQUIRE_LISTENER);
        }
        IEnumerator AfterMatchEnd()
        {
            yield return new WaitForSeconds(delayAfterMatchEnd);
            ShowExitButtonClientRPC();
        }
        [ClientRpc]
        void ShowExitButtonClientRPC()
        {
            Messenger.Broadcast(GameEventsPong.R_DISPLAY_EXIT_BUTTON, MessengerMode.REQUIRE_LISTENER);
        }

    }
}