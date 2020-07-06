using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode_menu : MonoBehaviour
{
    private Bullet bullet;
    GameObject sprite_bullet;
    private void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
        sprite_bullet = gameObject.transform.GetChild(1).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Угол между объектами
        float angle = Vector2.Angle(Vector2.up, position - transform.position);
        // Мгновенное вращение
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.x < position.x ? -angle : angle);
        // Вращение с задержкой
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, transform.position.x < position.x ? -angle : angle), 450f * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Vector3 position_cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Угол между объектами
        Vector3 position = sprite_bullet.transform.position;
        float angle = Vector2.Angle(Vector2.up, position_cam - bullet.transform.position);
        // Мгновенное вращение

        bullet.transform.eulerAngles = new Vector3(0f, 0f, bullet.transform.position.x < position_cam.x ? -angle : angle);

        //Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+0.6f, gameObject.transform.position.z);
        //position.x += 0.8F;
        Quaternion rotation = Quaternion.Euler(sprite_bullet.transform.rotation.x, sprite_bullet.transform.rotation.y, sprite_bullet.transform.rotation.z);
        //Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z));
        //position.x += gameObject.transform.position.x;
        Bullet newBullet = Instantiate(bullet, position, gameObject.transform.rotation) as Bullet;
        //float r = sprite_bullet.transform.rotation.z += 45;
        //Bullet newBullet = Instantiate(bullet, position, sprite_bullet.transform.rotation) as Bullet;
        newBullet.Parent = sprite_bullet;
        //newBullet.Direction = newBullet.transform.rotation * (gameObject.transform.localScale);
        newBullet.Direction = newBullet.transform.rotation * bullet.transform.localScale;
        //newBullet.Direction = bullet.transform.localScale.x;
    }
}
