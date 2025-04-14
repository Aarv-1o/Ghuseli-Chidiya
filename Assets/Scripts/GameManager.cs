using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int MaxNumberOfShots=3;
    private int _usedNumberofShots;

    private IconHandler _iconhandler;

    private List<Baddie> _baddies=new List<Baddie>();
    [SerializeField]private float checktime=0.5f;
    [SerializeField]private GameObject _restartscreenobject;
    [SerializeField]private Slingshothandler _slingshothandler;
   
    private void Awake(){
        if(instance==null){
            instance=this;
        }
        _iconhandler=FindObjectOfType<IconHandler>();
        Baddie[] baddies=FindObjectsOfType<Baddie>();
        for (int i=0;i<baddies.Length;i++){
            _baddies.Add(baddies[i]);
        }
    }

    public void UsedShots(){
        _usedNumberofShots++;
        _iconhandler.UseShot(_usedNumberofShots);
        Checkforlastshot();
       
    }
    public bool enoughshots(){
        if(_usedNumberofShots<MaxNumberOfShots){
            return true;
        }
        else{
            return false;
        }
    }

    private void Checkforlastshot(){
        if(_usedNumberofShots==MaxNumberOfShots){
            StartCoroutine(CheckAfterWaitTime());
        }
    }

    private IEnumerator CheckAfterWaitTime(){
        yield return new WaitForSeconds(checktime);
        if(_baddies.Count==0){
            Debug.Log("You Win");
        }
        else{
            Losegame();
        }
    }

    public void removebaddie(Baddie baddie){
        _baddies.Remove(baddie);
        checkfordeadbaddie();
       

    }
    private void checkfordeadbaddie(){
        if(_baddies.Count==0){
            Wingame();
        }
    }
    private void Wingame(){
        _restartscreenobject.SetActive(true);
        _slingshothandler.enabled=false;
    }
    public void Losegame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//PASSING THE SCENE INDEX
    }
    private void Loading(){}
}

