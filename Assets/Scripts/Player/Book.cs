using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Book : MonoBehaviour {
    [SerializeField] public GameObject pagePrefab;
    [SerializeField] public Renderer pageRightRenderer;
    [SerializeField] public Animator pageRightAnim;
    [SerializeField] public Renderer pageLeftRenderer;
    [SerializeField] public Animator pageLeftAnim;

    [SerializeField] private List<MaterialTuple> doublePages;
    [SerializeField] private int currentPage = 0;
    [SerializeField] private float pageTurnAnimationTime;
    [SerializeField] private float pagePosOffset = 0.005f;
    
    void Start() {
        GameObject pageRight = Instantiate(pagePrefab, transform);
        GameObject pageLeft = Instantiate(pagePrefab, transform);
        pageLeftAnim = pageLeft.GetComponent<Animator>();
        pageRightAnim = pageRight.GetComponent<Animator>();
        pageLeftRenderer = pageLeft.transform.GetChild(1).GetComponent<Renderer>();
        pageRightRenderer = pageRight.transform.GetChild(1).GetComponent<Renderer>();
        var leftMats = pageLeftRenderer.materials;
        var rightMats = pageRightRenderer.materials;

        
        leftMats[1] = doublePages[currentPage].a;
        if(currentPage > 0)
            leftMats[0] = doublePages[currentPage - 1].b;
        
        rightMats[0] = doublePages[currentPage].b;
        if(currentPage < doublePages.Count - 1)
            rightMats[1] = doublePages[currentPage + 1].a;
        
        pageLeftRenderer.materials  = leftMats;
        pageRightRenderer.materials = rightMats;        

        pageLeftAnim.speed = 0;
        pageRightAnim.speed = 0;
        
        pageLeftAnim.Play("TurnRight");
        pageRightAnim.Play("TurnLeft");
        
        GameManager.Input.CardUsage.CardSelectRelative.started += ProcessChange;
    }

    private void ProcessChange(InputAction.CallbackContext ctx) {
        bool right = ctx.ReadValue<float>() > 0;
        if (right) {
            if (currentPage >= doublePages.Count - 1) return;
            
            Debug.Log("right");
            GameObject movingPage = Instantiate(pagePrefab, transform);
            movingPage.transform.localPosition = Vector3.up * pagePosOffset * 2;
            Animator movingPageAnim = movingPage.GetComponent<Animator>();
            Renderer movingPageRenderer = movingPage.transform.GetChild(1).GetComponent<Renderer>();
            var mats = movingPageRenderer.materials;
            
            
            mats[0] = doublePages[currentPage].b;
            mats[1] = doublePages[currentPage+1].a;

            movingPageRenderer.materials = mats;
            
            movingPageAnim.Play("TurnLeft");
            
            StartCoroutine(SetPage(false, movingPageAnim, movingPageRenderer));
            
            currentPage++;

            var mat = pageRightRenderer.materials;
            mat[0] = doublePages[currentPage].b;
            pageRightRenderer.materials = mat;
        }
        else {
            if (currentPage <= 0) return;
            
            Debug.Log("left");
            GameObject movingPage = Instantiate(pagePrefab, transform);
            movingPage.transform.localPosition = Vector3.up * pagePosOffset * 2;
            Animator movingPageAnim = movingPage.GetComponent<Animator>();
            Renderer movingPageRenderer = movingPage.transform.GetChild(1).GetComponent<Renderer>();
            var mats = movingPageRenderer.materials;

            
            mats[0] = doublePages[currentPage-1].b;
            mats[1] = doublePages[currentPage].a;

            movingPageRenderer.materials = mats;
            
            movingPageAnim.Play("TurnRight");

            StartCoroutine(SetPage(true, movingPageAnim, movingPageRenderer));
            
            currentPage--;
            
            var mat = pageLeftRenderer.materials;
            mat[1] = doublePages[currentPage].a;
            pageLeftRenderer.materials = mat;
        }
    }

    private IEnumerator SetPage(bool right, Animator pageAnim, Renderer pageRenderer) {
        yield return new WaitForSeconds(pageTurnAnimationTime);

        pageAnim.transform.localPosition = Vector3.up * pagePosOffset;
        if (right) {
            Destroy(pageRightAnim.gameObject);
            pageRightAnim = pageAnim;
            pageRightRenderer = pageRenderer;
        }
        else {
            Destroy(pageLeftRenderer.gameObject);
            pageLeftAnim = pageAnim;
            pageLeftRenderer = pageRenderer;
        }
    }

    [Serializable]
    public class MaterialTuple {
        public Material a;
        public Material b;
    }
}
