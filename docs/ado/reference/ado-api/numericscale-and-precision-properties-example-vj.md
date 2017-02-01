---
title: "NumericScale and Precision Properties Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Precision property [ADO], VJ++ example"
  - "NumericScale property [ADO], VJ++ example"
ms.assetid: ca9f10d2-b5d1-449b-807b-649ef4fbf0bb
caps.latest.revision: 10
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# NumericScale and Precision Properties Example (VJ++)
This example uses the [NumericScale](../../../ado/reference/ado-api/numericscale-property-ado.md) and [Precision](../../../ado/reference/ado-api/precision-property-ado.md) properties to display the numeric scale and precision of fields in the ***Discounts*** table of the ***Pubs*** database.  
  
```  
// BeginNumericScaleJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class NumericScaleX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      NumericScaleX();  
      System.exit(0);  
   }  
  
   // NumericScaleX function  
  
   static void NumericScaleX()  
   {  
  
      // Define ADO Objects.  
      Recordset rstDiscounts = null;  
      Field fldTemp = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
          + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      int intLoop;  
  
      try  
      {  
         rstDiscounts = new Recordset();  
  
         // Open recordset.  
         rstDiscounts.open("Discounts", strCnn,  
                    AdoEnums.CursorType.FORWARDONLY,  
                    AdoEnums.LockType.READONLY,  
                    AdoEnums.CommandType.TABLE);  
  
         // Display numeric scale and precision of  
         // numeric and small integer fields.  
         for ( intLoop=0; intLoop <   
            rstDiscounts.getFields().getCount();intLoop++)  
         {  
            fldTemp = rstDiscounts.getFields().getItem(intLoop);  
            if((fldTemp.getType()== AdoEnums.DataType.NUMERIC) |  
               (fldTemp.getType()== AdoEnums.DataType.SMALLINT))  
            {  
               System.out.println("\nField: "  
                  + fldTemp.getName());  
               System.out.println("\nNumeric scale: "  
                  + fldTemp.getNumericScale());  
               System.out.println("\nPrecision: "  
                  + fldTemp.getPrecision());  
               System.out.println("\n\nPress <Enter> to continue..");  
               in.readLine();  
            }  
         }  
  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstDiscounts != null)  
         {  
            PrintProviderError(rstDiscounts.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
  
      }  
  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstDiscounts != null)  
            if (rstDiscounts.getState() == 1)  
               rstDiscounts.close();  
      }  
  
   }  
  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connections Errors collection.  
      com.ms.wfc.data.Error  ErrItem = null;  
      long nCount = 0;  
      int  i      = 0;  
  
      nCount = Cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if( nCount > 0);  
      {  
         // Collection ranges from 0 to nCount - 1  
         for (i = 0; i< nCount; i++)  
         {  
            ErrItem = Cnn1.getErrors().getItem(i);  
            System.out.println("\t Error number: " + ErrItem.getNumber()  
               + "\t" + ErrItem.getDescription() );  
         }  
      }  
  
   }  
  
   // PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
// EndNumericScaleJ  
  
```  
  
## See Also  
 [NumericScale Property (ADO)](../../../ado/reference/ado-api/numericscale-property-ado.md)   
 [Precision Property (ADO)](../../../ado/reference/ado-api/precision-property-ado.md)