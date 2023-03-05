using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TInstaller : MonoInstaller<TInstaller>
{
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private GameObject message;
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private TextMeshProUGUI highScore;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject chest;
    [SerializeField]
    private GameObject playableArea;
    public override void InstallBindings()
    {
        Container.Bind<Button>().FromInstance(yesButton).When(context => context.MemberName.Equals("yesButton"));
        Container.Bind<Button>().FromInstance(noButton).When(context => context.MemberName.Equals("noButton"));
        Container.Bind<TextMeshProUGUI>().FromInstance(title).When(context => context.MemberName.Equals("title"));
        Container.Bind<TextMeshProUGUI>().FromInstance(timer).When(context => context.MemberName.Equals("timer"));
        Container.Bind<TextMeshProUGUI>().FromInstance(highScore).When(context => context.MemberName.Equals("highscore"));
        Container.Bind<GameObject>().FromInstance(menu).When(context => context.MemberName.Equals("menu"));
        Container.Bind<GameObject>().FromInstance(message).When(context => context.MemberName.Equals("message"));
        Container.Bind<GameObject>().FromInstance(door).When(context => context.MemberName.Equals("door"));
        Container.Bind<GameObject>().FromInstance(chest).When(context => context.MemberName.Equals("chest"));
        Container.Bind<GameObject>().FromInstance(playableArea).When(context => context.MemberName.Equals("playableArea"));
    }
}