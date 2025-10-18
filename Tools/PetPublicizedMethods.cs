using LenenPets.Content.Pets;
using Microsoft.Xna.Framework;

namespace LenenPets.Tools;

public static class PetPublicizedMethods
{

    extension(BasicLenenPet pet) 
    {
        /// <summary>
        /// 常规移动AI
        /// </summary>
        /// <param name="point">移动到的位置</param>
        /// <param name="speed">移动速度</param>
        public void MoveToPoint( Vector2 point, float speed, Vector2 center = default)
        {
            if (center == default)
            {
                center = pet.Owner.MountedCenter;
            }

            Vector2 pos = center + point;
            float dist = Vector2.Distance(pet.Projectile.Center, pos);
            if (dist > 1200f)
                pet.Projectile.Center = pos;

            Vector2 vel = pos - pet.Projectile.Center;
            float closeValue = 1f;

            if (dist < closeValue)
                pet.Projectile.velocity *= 0.25f;

            if (vel != Vector2.Zero)
            {
                if (vel.Length() < closeValue)
                    pet.Projectile.velocity = vel;
                else
                    pet.Projectile.velocity = vel * 0.01f * speed;
            }
        }
        /// <summary>
        /// 常规移动AI：2
        /// </summary>
        /// <param name="point">移动到的位置</param>
        /// <param name="speed">移动速度</param>
        public void MoveToPoint2( Vector2 point, float speed)
        {
            Vector2 targetPos = pet.Owner.Center + point;
            Vector2 targetVel = targetPos - pet.Projectile.Center;

            float length = (targetPos == Vector2.Zero) ? 0f : pet.Projectile.Distance(targetPos);
            float distanceLimit = MathHelper.Lerp(0f, speed, length / 200f);
            float scaledSpeed = MathHelper.SmoothStep(0f, speed, distanceLimit);

            if (scaledSpeed <= 0.1f)
                scaledSpeed = 0f;

            pet.Projectile.velocity = targetVel.SafeNormalize(Vector2.One) * scaledSpeed;
        }
        /// <summary>
        /// 设置转向
        /// </summary>
        /// <param name="dist">设置与玩家同向的最小距离</param>
        public void ChangeDir( float dist = 100)
        {
            if (pet.Projectile.Distance(pet.Owner.Center) <= dist)
            {
                pet.Projectile.spriteDirection = pet.Owner.direction;
            }
            else
            {
                if (pet.Projectile.velocity.X > 0.25f)
                {
                    pet.Projectile.spriteDirection = 1;
                }
                else if (pet.Projectile.velocity.X < -0.25f)
                {
                    pet.Projectile.spriteDirection = -1;
                }
            }
        }
    }

}
