---
title: "CompareBookmarks Method Example (VJ++) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "CompareBookmarks method [ADO], VJ++ example"
ms.assetid: 3c679a15-e924-49a5-8f3a-38a8266064f8
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# CompareBookmarks Method Example (VJ++)
This example demonstrates the [CompareBookmarks](../../../ado/reference/ado-api/comparebookmarks-method-ado.md) method. The relative value of bookmarks is seldom needed unless a particular bookmark is somehow special.  
  
 Designate a random row of a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) derived from the ***Authors*** table as the target of a search. Then display the position of each row relative to that target.  
  
```  
// BeginCompareBookmarksJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
import com.ms.com.*;  
  
public class CompareBookmarksX //  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      CompareBookmarksX();  
      System.exit(0);  
   }  
  
   // CompareBookmarksX function  
  
   static void CompareBookmarksX()  
   {  
      // Define ADO Objects.  
      Recordset rstAuthors = null;  
  
      // Declarations.  
      BufferedReader in =  
         new BufferedReader (new InputStreamReader(System.in));  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';" +  
        "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      int intCount;  
      Variant varTarget = null;  
      int intResult;  
      String strAns;  
      int intDisplaySize = 15;  
  
      try  
      {  
         rstAuthors = new Recordset();  
         rstAuthors.open("SELECT * FROM authors",  
            strCnn,  
            AdoEnums.CursorType.STATIC,  
            AdoEnums.LockType.READONLY,  
            AdoEnums.CommandType.TEXT);  
         intCount = rstAuthors.getRecordCount();  
         System.out.println("Rows in the Recordset = " +  
            Integer.toString(intCount));  
  
         // Exit if an empty recordset.  
         if(intCount == 0)  
            System.exit(0);  
  
         // Randomize.  
         intCount = (int)(intCount * Math.random());  
         // Get position between 0 and count-1.  
         System.out.println("\nRandomly chosen row position = " +  
            Integer.toString(intCount)+ "\n");  
         rstAuthors.move(intCount,new Integer(AdoEnums.Bookmark.FIRST));          // Move row to random position.  
         varTarget = (Variant)rstAuthors.getBookmark();  
         // Remember the mystery row.  
         intCount = 0;  
         rstAuthors.moveFirst();  
  
         // Loop through recordset.  
         while(!rstAuthors.getEOF())  
         {  
            intResult = rstAuthors.compareBookmarks  
               ((Variant)rstAuthors.getBookmark(), varTarget);  
            if(intResult == AdoEnums.Compare.NOTEQUAL)  
               System.out.println("Row " +  
                  Integer.toString(intCount) +  
                  ": Bookmarks are not equal.");  
            else if(intResult == AdoEnums.Compare.NOTCOMPARABLE)  
               System.out.println("Row " +  
                  Integer.toString(intCount) +  
                  ": Bookmarks are not comparable.");  
            else  
            {  
               switch(intResult)  
               {  
               case AdoEnums.Compare.LESSTHAN :  
                  strAns = "less than";  
                  break;  
               case AdoEnums.Compare.EQUAL :  
                  strAns = "equal to";  
                  break;  
               case AdoEnums.Compare.GREATERTHAN :  
                  strAns = "greater than";  
                  break;  
               default :  
                  strAns = "in error comparing to";  
                  break;  
               }  
               System.out.println("Row position " +  
                  Integer.toString(intCount) +  
                  " is " + strAns + " the target.");  
            }  
            if(intCount % intDisplaySize == 0 && intCount > 0)  
            {  
               System.out.println("\nPress <Enter> to continue..");  
               in.readLine();  
            }  
            intCount++;  
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
         if (rstAuthors != null)  
            if (rstAuthors.getState() == 1)  
               rstAuthors.close();  
      }  
   }  
  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connection's Errors collection.  
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
  
// EndCompareBookmarksJ  
```  
  
## See Also  
 [CompareBookmarks Method (ADO)](../../../ado/reference/ado-api/comparebookmarks-method-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)