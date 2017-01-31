---
title: "NextRecordset Method Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "NextRecordset method [ADO], VJ++ example"
ms.assetid: 7948eefb-f5cc-4e74-b2f4-39033b46243d
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# NextRecordset Method Example (VJ++)
This example uses the [NextRecordset](../../../ado/reference/ado-api/nextrecordset-method-ado.md) method to view the data in a recordset that uses a compound command statement made up of three separate **SELECT** statements.  
  
```  
// BeginNextRecordsetJ  
import java.io.*;  
import com.ms.wfc.data.*;  
  
public class NextRecordsetX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      NextRecordsetX();  
      System.exit(0);  
   }  
   // NextRecordsetX Function  
   static void NextRecordsetX()  
   {  
      // Define ADO Object  
      Recordset rstCompound = null;  
  
      // Declarations  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
      String strCnn;  
      int intCount;  
      int intDisplayRecords = 15;  
      int intRecordCount;  
  
      try  
      {  
         // Open compound recordset.  
         strCnn = "Provider='sqloledb';Data Source='MySqlServer';" +  
             "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
         rstCompound = new Recordset();  
         rstCompound.open("select * from Authors;" +  
             "select * from stores;" +  
             "select * from jobs", strCnn, AdoEnums.CursorType.FORWARDONLY,   
            AdoEnums.LockType.READONLY,AdoEnums.CommandType.TEXT);  
  
         // Display results from each select statement.  
         intCount=1;  
  
         while (rstCompound != null)  
         {    
            System.out.println(  
               "Contents of recordset #" + intCount + "\n");  
            intRecordCount = 0;  
  
            while(!rstCompound.getEOF())  
               {  
                  System.out.println(  
                     rstCompound.getField(0).getString()+" " +   
                     rstCompound.getField(1).getString());  
                  intRecordCount++;  
  
                  rstCompound.moveNext();  
                  if ( intRecordCount == intDisplayRecords)  
                  {  
                     System.out.println("\nPress <Enter> to continue..");  
                     in.readLine();  
                     intRecordCount = 0;  
                  }  
               }  
            System.out.println("\nPress <Enter> to continue..");  
            in.readLine();  
  
            rstCompound = rstCompound.nextRecordset();   
  
            intCount++;  
         }  
      }  
      // System read requires this catch.  
      catch(java.io.IOException je)  
      {  
         PrintIOError(je);  
      }  
      catch(AdoException ae)  
      {  
         // Notify the user of any errors that result from ADO.  
  
         // As passing a recordset. check for the null pointer first  
         if(rstCompound!=null)  
         {  
            PrintProviderError(rstCompound.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
      catch(java.lang.NullPointerException ne)  
      {  
         System.out.println("Error Description: " + ne.getMessage());  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstCompound != null)  
            if (rstCompound.getState() == 1)  
               rstCompound.close();  
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
// EndNextRecordsetJ  
  
```  
  
## See Also  
 [NextRecordset Method (ADO)](../../../ado/reference/ado-api/nextrecordset-method-ado.md)