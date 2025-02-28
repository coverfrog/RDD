using UnityEngine;

public enum RddScenarioType
{
    Intro,
    MainMenu,
}

public interface IRddScenario
{
    public void OnBegin();

    public void OnEnd();
}

public partial class RddManager 
{
    private void ScenarioSet()
    {
        
    }

    private void ScenarioStart()
    {
        
    }
}
