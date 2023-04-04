using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    public GameObject currentTarget;
    public bool lockOn;
    private Vector3 targetPos;
    private Vector3 targetPosFromCamera;
    Quaternion rotGoal;
    Quaternion rotGoalFromCamera;
    [SerializeField] float rotationTime = 0.75f;
    //[SerializeField] private GameObject mainCamera;


    void Update() {
        if (currentTarget !=null && currentTarget.activeSelf == true){
            // Desativa o movimento do personagem
            GetComponent<MouseLook>().enabled = false;
            GetComponent<FPSInput>().enabled = false; 
          

            // Pega a posição do inimigo
            targetPos = (currentTarget.transform.position - transform.position).normalized;
            rotGoal = Quaternion.LookRotation(targetPos);
            

            // Calcula a velocidade de rotação
            float angle = Quaternion.Angle(transform.rotation, rotGoal);
            float speed = angle / rotationTime;


            // Determina quais eixos vão rotacionas (personagem)
            Vector3 newRotGoal = new Vector3(transform.rotation.x, rotGoal.y, transform.rotation.z);
            Quaternion newQuaternion = new Quaternion(newRotGoal.x, newRotGoal.y, newRotGoal.z, rotGoal.w);            
            
            // Rotaciona personagem
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newQuaternion, speed * Time.deltaTime);

            lockOn = true;
                    
        }
        else {
            // Ativa o movimento do personagem
            GetComponent<MouseLook>().enabled = true;
            GetComponent<FPSInput>().enabled = true;
            lockOn = false;
        }
    }
    
    Quaternion GetRotationGoal(Vector3 originPosition) {
        // Pega a posição do inimigo
        targetPos = (currentTarget.transform.position - originPosition).normalized;
        Quaternion rotGoal = Quaternion.LookRotation(targetPos);
        return(rotGoal);
    }
  

    public void LockOn()
    {
        lockOn = !lockOn;
    }

} 