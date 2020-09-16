using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {

        print("121");

        var notification = new AndroidNotification();
        notification.Title = "Test Title";
        notification.Text = "Test messadge";
        notification.FireTime = System.DateTime.Now.AddMinutes(0.1f);

        AndroidNotificationCenter.SendNotification(notification, "Test");

    }

    // Start is called before the first frame update
    void Start()
    {

        AndroidNotificationChannel channel = new AndroidNotificationChannel("Test", "Mage Hero", "It's a test notification", Importance.Default);
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

    }

    


}
