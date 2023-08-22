using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOutFitsManager : MonoBehaviour
{
   [Header(" Reference of OutFitList SCs")]
   public OutFitSC_Container hairsOutFitList;
   public OutFitSC_Container pantsOutFitList;
   public OutFitSC_Container shirtsOutFitList;
   public OutFitSC_Container dressesOutFitList;
   public OutFitSC_Container shoesOutFitList;


   [Header(" References of main Character")]
   public SpriteRenderer hairOutFitHolder;
   public SpriteRenderer pantOutFitHolder;
   public SpriteRenderer shirtOutFitHolder;
   public SpriteRenderer dressOutFitHolder;
   public SpriteRenderer shoesOutFitHolder;
   public ParticleSystem shoesParticles;

   [Header(" References of Default OutFits")]
   public Sprite defaultDressOutFitSp;
   public Sprite defaultShirtOutFitSp;
   public Sprite defaultPantOutFitSp;
   public Sprite defaultHairOutFitSp;
   [Header("UI References")] 
   public Text outFitPriceBuyPanel;
   public GameObject outFitBuyPanel;
   public Image outFiImgBuy;
   public Image outColorFiImgBuy;
   public outFitSelector outFitSelectorTemplate;
   public Transform[] mainCatBtns;
   public Transform mainCatSelectIcon;


   public Transform outFitSelectIcon;
   
   List<outFitSelector> outFitBtnList=new List<outFitSelector>();

   private OutFitSC lastOutFitSelected;
   private SpriteRenderer lastCatHolderSelected;


   public DressUpLevelSC testDressUpLevel;

   public static PlayerOutFitsManager instanceOutFitsManager;

   private void Awake()
   {
       instanceOutFitsManager = this;
   }

   private void Start()
   {
//       ClickOnOutFitMainCategory(0);
   }

   public void ClickOnOutFitMainCategory(int index)
   {
       // Code for Select Icon Pos
       mainCatSelectIcon.SetParent(mainCatBtns[index]);
       mainCatSelectIcon.localPosition=Vector3.zero;
       mainCatSelectIcon.SetAsFirstSibling();
       mainCatSelectIcon.gameObject.SetActive(true);
       switch (index)
       {
           case 0: // Hair Category OutFits
               AssignOutFitsToButtons(hairsOutFitList);
               lastCatHolderSelected = hairOutFitHolder;
               break;
           case 1: // Dress Category OutFits
               AssignOutFitsToButtons(dressesOutFitList);
               lastCatHolderSelected = dressOutFitHolder;
               break;
           case 2: // Shirt Category OutFits
               AssignOutFitsToButtons(shirtsOutFitList);
               lastCatHolderSelected = shirtOutFitHolder;
               break;
           case 3: // pant Category OutFits
               AssignOutFitsToButtons(pantsOutFitList);
               lastCatHolderSelected = pantOutFitHolder;
               break;
           case 4: // Shoes Category OutFits
               AssignOutFitsToButtons(shoesOutFitList);
               lastCatHolderSelected = shoesOutFitHolder;
               break;
       }
       CheckForWearingLockApparel();
   }
   
   public void AssignOutFitsToButtons(OutFitSC_Container ouFitScContainer)
   {
       outFitSelectIcon.gameObject.SetActive(false);
       int count = 0;
        
        // Reset Apparelselector list before assign
        foreach (outFitSelector apperalSelector in outFitBtnList)
        {
            apperalSelector.gameObject.SetActive(false);
        }
        
        foreach (OutFitSC outFit in ouFitScContainer.OutFitList)
        {
            if (count < outFitBtnList.Count)
            {
                outFitBtnList[count].currentOutFit = outFit;
                outFitBtnList[count].UpdateOutFitState();
                outFitBtnList[count].gameObject.SetActive(true);
            }
            else
            {
                GameObject apparelC = Instantiate(outFitSelectorTemplate.gameObject, outFitSelectorTemplate.transform.parent);
                apparelC.SetActive(true);
                outFitSelector tempApperalSelector = apparelC.GetComponent<outFitSelector>();
                tempApperalSelector.currentOutFit = outFit;
                tempApperalSelector.UpdateOutFitState();
                outFitBtnList.Add(tempApperalSelector);
            }
            count++;
        }
   }
   
   
    // changing the category 
    public void CheckForWearingLockApparel()
    {
        if(lastOutFitSelected==null)
            return;
        if (!lastOutFitSelected.isUnlockOutFit)
        {
            switch (lastOutFitSelected.outFitCat)
            {
                // Here we assign or turn off holder of item which is not unlocked
                case OutFitMainCat.HairOutFit:
                    hairOutFitHolder.gameObject.SetActive(false);
                    break;
                case OutFitMainCat.DressOutFit:
                    dressOutFitHolder.sprite = defaultDressOutFitSp;
                    break;
                case OutFitMainCat.ShirtOutFit:
                    shirtOutFitHolder.sprite = defaultShirtOutFitSp;
                    break;
                case OutFitMainCat.PantOutFit:
                    pantOutFitHolder.sprite = defaultPantOutFitSp;
                    break;
                case OutFitMainCat.ShoesOutFit:
                    shoesOutFitHolder.gameObject.SetActive(false);
                    break;
            }
            
        }
    }
    

    private outFitSelector lastOutFitSelectorClicked;
    public void PlaceOutFitOnHolder(outFitSelector outFitSelector)
    {
        lastOutFitSelectorClicked = outFitSelector;
        OutFitSC outFitSc = outFitSelector.currentOutFit;
        outFitSelectIcon.transform.SetParent(outFitSelector.transform);
        outFitSelectIcon.SetAsFirstSibling();
        outFitSelectIcon.localPosition=Vector3.zero;
        outFitSelectIcon.gameObject.SetActive(true);
        switch (outFitSc.outFitCat)
        {
            case OutFitMainCat.HairOutFit:
                if (outFitSc.isUnlockOutFit)
                {
                    PlaceOutFitOnHolderTem(hairOutFitHolder,outFitSelector);
                }
                else
                {
                    OpenBuyPanel();
                }
                break;
            case OutFitMainCat.DressOutFit:
                if (outFitSc.isUnlockOutFit)
                {
                    PlaceOutFitOnHolderTem(dressOutFitHolder,outFitSelector);
                }
                else
                {
                    OpenBuyPanel();
                }
                break;
            case OutFitMainCat.ShirtOutFit:
                if (outFitSc.isUnlockOutFit)
                {
                    PlaceOutFitOnHolderTem(shirtOutFitHolder,outFitSelector);
                }
                else
                {
                    OpenBuyPanel();
                }
                break;
            case OutFitMainCat.PantOutFit:
                if (outFitSc.isUnlockOutFit)
                {
                    PlaceOutFitOnHolderTem(pantOutFitHolder,outFitSelector);
                }
                else
                {
                    OpenBuyPanel();
                }
                break;
            case OutFitMainCat.ShoesOutFit:
                if (outFitSc.isUnlockOutFit)
                {
                    PlaceOutFitOnHolderTem(shoesOutFitHolder,outFitSelector);
                }
                else
                {
                    OpenBuyPanel();
                }
                break;

        }
    }

   public void PlaceOutFitOnHolderTem(SpriteRenderer holder, outFitSelector activeOutFitSelector ,GameObject particles=null)
    {
        holder.gameObject.SetActive(true);
        holder.sprite = activeOutFitSelector.currentOutFit.outFitOriginalImg;
        if (activeOutFitSelector.currentOutFit == lastOutFitSelected)
        {
            if ( activeOutFitSelector.currentOutFit.outFitCat == OutFitMainCat.DressOutFit )
            {
                shirtOutFitHolder.gameObject.SetActive(false);
                pantOutFitHolder.gameObject.SetActive(false);
                dressOutFitHolder.sprite = defaultDressOutFitSp;

            }
            else if (  activeOutFitSelector.currentOutFit.outFitCat == OutFitMainCat.ShirtOutFit )
            {
                dressOutFitHolder.gameObject.SetActive(false);
                shirtOutFitHolder.sprite = defaultShirtOutFitSp;
            }
            else if ( activeOutFitSelector.currentOutFit.outFitCat == OutFitMainCat.PantOutFit )
            {
                dressOutFitHolder.gameObject.SetActive(false);
                pantOutFitHolder.sprite = defaultPantOutFitSp;
            }
            else if ( activeOutFitSelector.currentOutFit.outFitCat == OutFitMainCat.HairOutFit )
            {
                hairOutFitHolder.sprite = defaultHairOutFitSp;
            }
            else
            {
                holder.gameObject.SetActive(false); 
            }
            lastOutFitSelected = null;
            return;
        }

        if (activeOutFitSelector.currentOutFit.outFitCat == OutFitMainCat.DressOutFit)
        {
            shirtOutFitHolder.gameObject.SetActive(false);
            pantOutFitHolder.gameObject.SetActive(false);
        }
        else  if(activeOutFitSelector.currentOutFit.outFitCat == OutFitMainCat.ShirtOutFit || activeOutFitSelector.currentOutFit.outFitCat == OutFitMainCat.PantOutFit )
        {
            dressOutFitHolder.gameObject.SetActive(false);
            shirtOutFitHolder.gameObject.SetActive(true);
            pantOutFitHolder.gameObject.SetActive(true);
        }
        lastOutFitSelected = activeOutFitSelector.currentOutFit;

    }

   void OpenBuyPanel()
   {
       outFitBuyPanel.SetActive(true);
       outFiImgBuy.sprite = lastOutFitSelectorClicked.currentOutFit.outFitIcon;
       outFitPriceBuyPanel.text =  lastOutFitSelectorClicked.currentOutFit.priceOfOutFit + "";
       outFitBuyPanel.transform.parent.gameObject.SetActive(true);
   }

   public void CloseBuyPanel()
   {
       outFitBuyPanel.SetActive(false);
       outFitBuyPanel.transform.parent.gameObject.SetActive(false);
   }

   public void BuyOutFit()
   {
       if (GameDataSaveHandler.TotalCoinsNo >= lastOutFitSelectorClicked.currentOutFit.priceOfOutFit)
       {
           GameDataSaveHandler.TotalCoinsNo = -lastOutFitSelectorClicked.currentOutFit.priceOfOutFit;
           lastOutFitSelectorClicked.currentOutFit.isUnlockOutFit = true;
           GameDataSaveHandler.Instance.SaveGameData();
           RefreshDataOfOutFit();
           CloseBuyPanel();
           EventHandler.InvokeUpdateCoinsEvent();
           PlaceOutFitOnHolder(lastOutFitSelectorClicked);
       }
   }

   void RefreshDataOfOutFit()
   {
       foreach (outFitSelector outFitSelector in outFitBtnList)
       {
           outFitSelector.UpdateOutFitState();
       }
   }
   

   

}
public enum OutFitMainCat
{
    DressOutFit,HairOutFit,ShoesOutFit,PantOutFit,ShirtOutFit,WarmOutFit
}