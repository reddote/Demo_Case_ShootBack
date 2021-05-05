using UnityEngine;

public class BulletPooling : ObjectPooling<Bullet>{
    
    public void Shoot(float damage){
        StackPop().Damage = damage;
    }

    public void ReturnStackAccessor(Bullet bullet){
        ReturnTStack(bullet);
    }
    
}