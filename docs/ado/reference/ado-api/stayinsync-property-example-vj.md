---
title: "StayInSync Property Example (VJ++) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "StayInSync property [ADO], VJ++ example"
ms.assetid: 48e2f748-e8e0-4cbb-8133-b96aa06c6324
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# StayInSync Property Example (VJ++)
This example demonstrates how the [StayInSync](../../../ado/reference/ado-api/stayinsync-property.md) property facilitates accessing rows in a hierarchical [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
 The outer loop displays each author's first and last name, state, and identification. The appended **Recordset** for each row is retrieved from the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection and automatically assigned to **rstTitleAuthor** by the **StayInSync** property whenever the parent **Recordset** moves to a new row. The inner loop displays four fields from each row in the appended recordset.  
  
```  
// BeginStayInSyncJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
import com.ms.com.*;  
  
public class StayInSyncX  
{  
    // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      StayInSyncX();  
      System.exit(0);  
   }  
  
   // StayInSyncX function  
  
   static void StayInSyncX()  
   {  
  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Recordset rstAuthors = null;  
      Recordset rstTitleAuthor = null;  
  
      // Declarations.  
      BufferedReader in = new   
         BufferedReader (new InputStreamReader(System.in));  
      String strCnn = "Provider=MSDataShape;" +   
         "Data Provider='sqloledb';Data Source='MySqlServer';" +   
         "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      try  
      {  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
         rstAuthors = new Recordset();  
         rstAuthors.setStayInSync(true);  
         rstAuthors.open("SHAPE  {select * from Authors} " +  
                "APPEND ({select * from titleauthor}" +  
                "RELATE au_id TO au_id) AS chapTitleAuthor",  
               cnConn1,  
               AdoEnums.CursorType.STATIC,  
               AdoEnums.LockType.READONLY,  
               AdoEnums.CommandType.TEXT);  
  
         Variant varRstTitleAuthor = rstAuthors.getFields().  
            getItem("chapTitleAuthor").getValue();  
         rstTitleAuthor =new Recordset(varRstTitleAuthor.toObject());  
         int intCount =0;  
         while(!rstAuthors.getEOF())  
         {  
            System.out.println("\n" +  
               rstAuthors.getField("au_fname").getString() + " " +  
               rstAuthors.getField("au_lname").getString() + " " +  
                rstAuthors.getField("state").getString() + " " +  
                rstAuthors.getField("au_id").getString());  
            while(!rstTitleAuthor.getEOF())  
            {  
               System.out.println(rstTitleAuthor.getField(0).  
                  getString() + " " +  
                  rstTitleAuthor.getField(1).getString() + " " +  
                  rstTitleAuthor.getField(2).getString() + " " +  
                  rstTitleAuthor.getField(3).getString());  
               rstTitleAuthor.moveNext();  
            }  
            if(++intCount % 5 == 0)  
            {  
               System.out.println("\nPress <Enter> to continue..");  
               in.readLine();  
            }  
            rstAuthors.moveNext();  
         }  
  
         System.out.println("\nPress <Enter> to continue..");  
         in.readLine();  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstAuthors != null)  
         {  
            PrintProviderError(rstAuthors.getActiveConnection());  
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
         if (rstTitleAuthor != null)  
            if (rstTitleAuthor.getState() == 1)  
               rstTitleAuthor.close();     
         if (rstAuthors != null)  
            if (rstAuthors.getState() == 1)  
               rstAuthors.close();     
         if (cnConn1 != null)  
            if (cnConn1.getState() == 1)  
               cnConn1.close();  
      }  
   }  
  
   // PrintProviderError Function  
   static void PrintProviderError(Connection cnn1)  
   {  
      // Print Provider Errors from Connection Object.  
      // ErrItem is an item object in the Connections Errors Collection.  
      com.ms.wfc.data.Error               ErrItem = null;  
      long                                 nCount = 0;  
      int                                       i = 0;  
  
      nCount = cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if ( nCount > 0)  
      {  
         // Collection ranges from 0 to nCount-1.  
         for ( i=0;i<nCount; i++)  
         {  
            ErrItem = cnn1.getErrors().getItem(i);  
            System.out.println("\t Error Number: " + ErrItem.getNumber()   
               + "\t" + ErrItem.getDescription());  
         }  
      }  
   }  
   // PrintIOError Function  
   static void PrintIOError(java.io.IOException je)  
   {  
      System.out.println("Error: \n");  
      System.out.println("\t Source: " + je.getClass() + "\n");  
      System.out.println("\t Description: "+ je.getMessage() + "\n");  
   }  
}  
// EndStayInSyncJ  
  
```  
  
## See Also  
 [Fields Collection (ADO)](../../../ado/reference/ado-api/fields-collection-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)   
 [StayInSync Property](../../../ado/reference/ado-api/stayinsync-property.md)