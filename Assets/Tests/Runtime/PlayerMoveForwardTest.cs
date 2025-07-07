using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace SuperRunner
{
    public class PlayerMoveForwardTest
    {
        [UnityTest]
        public IEnumerator Player_Moves_Forward_Automatically()
        {
            // Arrange
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
            yield return null;

            var player = GameObject.FindGameObjectWithTag("Player");
            Assert.IsNotNull(player, "Player not found in the scene.");

            Vector3 startPos = player.transform.position;

            // Act
            yield return new WaitForSeconds(1f);

            Vector3 endPos = player.transform.position;

            // Assert
            Assert.Greater(endPos.z, startPos.z,
                $"Player did not move forward. StartZ: {startPos.z}, EndZ: {endPos.z}");
        }
    }
}
