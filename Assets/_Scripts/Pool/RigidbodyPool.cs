using RiaShooter.Scripts.Common;
using System;
using UnityEngine;

namespace RiaShooter.Scripts.Pool
{
    public partial class RigidbodyPool : ObjectPool<Rigidbody>
    {
        [SerializeField] CustomVector3 defaultForce;
        [SerializeField] CustomVector3 defaultTorque;

        /// <summary>
        /// Вращает и двигает свободный риджид из пула
        /// </summary>
        /// <param name="spawnPoint">Точка появления риджида</param>
        /// <param name="force">Сила, придаваемая при включении. Если default, то берутся параметры из настроек пула</param>
        /// <param name="torque">Вращение, придаваемое при включении. Если default, то берутся параметры из настроек пула</param>
        public void GetRigidFromPool(Transform spawnPoint, Quaternion customRotation = default, Vector3 force = default, Vector3 torque = default)
        {
            Rigidbody rigid = GetObjectFromPoolInternal();

            rigid.isKinematic = true;
            rigid.transform.position = spawnPoint.position;
            rigid.transform.rotation = customRotation == default ? spawnPoint.rotation : customRotation;
            rigid.isKinematic = false;

            rigid.gameObject.SetActive(true);
            rigid.AddForce(force == default ? spawnPoint.TransformDirection(defaultForce) : force);
            rigid.AddTorque(torque == default ? spawnPoint.TransformDirection(defaultTorque) : torque);
        }
    }
}