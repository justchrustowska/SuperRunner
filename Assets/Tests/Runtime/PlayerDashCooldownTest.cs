using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace SuperRunner
{
    public class PlayerDashCooldownTest
    {
        [UnityTest]
        public IEnumerator Dash_Cooldown_Test()
        {
            // Arrange
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
            yield return null;

            var player = GameObject.FindObjectOfType<PlayerMoveSystem>();
            Assert.IsNotNull(player, "Player not found.");

            var dashField = player.GetType().GetField("_dashCooldown",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.IsNotNull(dashField, "Field _dashCooldown not found.");

            // Act
            player.SendMessage("Dash");
            yield return new WaitForSeconds(0.1f);
            
            player.SendMessage("Dash");
            yield return new WaitForSeconds(0.1f);

            bool isOnCooldown = (bool)dashField.GetValue(player);

            // Assert
            Assert.IsTrue(isOnCooldown, "Player was able to dash again before cooldown ended.");
        }
    }
}
