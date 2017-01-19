---
title: "Supports Method Example (VJ++) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Supports method [ADO], VJ++ example"
ms.assetid: eb7a5d97-0f3c-4bd4-b66d-cd1c454c4a93
caps.latest.revision: 11
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Supports Method Example (VJ++)
This example uses the [Supports](../../../ado/reference/ado-api/supports-method.md) method to display the options supported by a recordset opened with different cursor types. The DisplaySupport function is required for this example to run.  
  
```  
// BeginSupportsJ  
import com.ms.wfc.data.*;  
import java.io.*;  
  
public class SupportsX  
{  
   // The main entry point of the application.  
  
   public static void main (String[] args)  
   {  
      SupportsX();  
      System.exit(0);  
   }  
   // SupportsX Function  
  
   static void SupportsX()  
   {  
      // Define ADO Objects.  
      Recordset rstTitles = null;  
  
      // Declarations.  
      int[] aintCursorType = new int[4];  
      String strCnn;  
      int intIndex;  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
  
      try  
      {  
         // Open connections.  
         strCnn = "Provider='sqloledb';Data Source='MySqlServer';"+  
                "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
         // Fill array with CursorType constants.  
         aintCursorType[0] = AdoEnums.CursorType.FORWARDONLY;  
         aintCursorType[1] = AdoEnums.CursorType.KEYSET;  
         aintCursorType[2] = AdoEnums.CursorType.DYNAMIC;  
         aintCursorType[3] = AdoEnums.CursorType.STATIC;  
  
         // Open recordset using each CursorType and  
         // Optimistic Locking.  Then call the DisplaySupport  
         // procedure to display the supported options.  
         for(intIndex = 0; intIndex < 4; intIndex++)  
         {  
            rstTitles = new Recordset();  
            rstTitles.setCursorType(aintCursorType[intIndex]);  
            rstTitles.setLockType(AdoEnums.LockType.OPTIMISTIC);  
            rstTitles.open("Titles", strCnn, aintCursorType[intIndex],   
               AdoEnums.LockType.OPTIMISTIC, AdoEnums.CommandType.TABLE);  
  
            switch(aintCursorType[intIndex])  
            {  
            case AdoEnums.CursorType.FORWARDONLY:  
               System.out.println("ForwardOnly cursor supports:");  
               break;  
            case AdoEnums.CursorType.KEYSET:  
               System.out.println("Keyset cursor supports:");  
               break;  
            case AdoEnums.CursorType.DYNAMIC:  
               System.out.println("Dynamic cursor supports:");  
               break;  
            case AdoEnums.CursorType.STATIC:  
               System.out.println("Static cursor supports:");  
               break;  
            default:  
               break;  
            }  
            DisplaySupport(rstTitles);  
            System.out.println("Press <Enter> to continue..");  
            in.readLine();  
         }  
      }  
      catch(AdoException ae)  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstTitles!= null)  
         {  
            PrintProviderError(rstTitles.getActiveConnection());  
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
         if (rstTitles != null)  
            if (rstTitles.getState() == 1)  
               rstTitles.close();  
      }  
   }  
   // DisplaySupport Function  
  
   static void DisplaySupport( Recordset rstTemp )  
   {  
      long[] alngConstants = new long[11];  
      boolean booSupports;  
      int intIndex;  
      try  
      {  
         // Fill array with cursor option constants.  
         alngConstants[0]  = AdoEnums.CursorOption.ADDNEW;  
         alngConstants[1]  = AdoEnums.CursorOption.APPROXPOSITION;  
         alngConstants[2]  = AdoEnums.CursorOption.BOOKMARK;  
         alngConstants[3]  = AdoEnums.CursorOption.DELETE;  
         alngConstants[4]  = AdoEnums.CursorOption.FIND;  
         alngConstants[5]  = AdoEnums.CursorOption.HOLDRECORDS;  
         alngConstants[6]  = AdoEnums.CursorOption.MOVEPREVIOUS;  
         alngConstants[7]  = AdoEnums.CursorOption.NOTIFY;  
         alngConstants[8]  = AdoEnums.CursorOption.RESYNC;  
         alngConstants[9]  = AdoEnums.CursorOption.UPDATE;  
         alngConstants[10] = AdoEnums.CursorOption.UPDATEBATCH;  
  
         for(intIndex = 0; intIndex <= 10; intIndex++)  
         {  
            booSupports = rstTemp.supports((int)alngConstants[intIndex]);  
            if (booSupports)  
            {  
               switch((int)alngConstants[intIndex])  
               {  
               case AdoEnums.CursorOption.ADDNEW:  
                     System.out.println("    AddNew");  
                     break;  
               case AdoEnums.CursorOption.APPROXPOSITION:  
                     System.out.println(  
                        "    AbsolutePosition and AbsolutePage");  
                     break;  
               case AdoEnums.CursorOption.BOOKMARK:  
                     System.out.println("    Bookmark");  
                     break;  
               case AdoEnums.CursorOption.DELETE:  
                     System.out.println("    Delete");  
                     break;  
               case AdoEnums.CursorOption.FIND:  
                     System.out.println("    Find");  
                     break;  
               case AdoEnums.CursorOption.HOLDRECORDS:  
                     System.out.println("    Holding Records");  
                     break;  
               case AdoEnums.CursorOption.MOVEPREVIOUS:  
                     System.out.println("    MovePrevious and Move");  
                     break;  
               case AdoEnums.CursorOption.NOTIFY:  
                     System.out.println("    Notifications");  
                     break;  
               case AdoEnums.CursorOption.RESYNC:  
                     System.out.println("    Resyncing Data");  
                     break;  
               case AdoEnums.CursorOption.UPDATE:  
                     System.out.println("    Update");  
                     break;  
               case AdoEnums.CursorOption.UPDATEBATCH:  
                     System.out.println("    Batch Updating");  
                     break;  
               default:  
                     break;  
               }  
            }  
         }  
      }  
      catch(AdoException ae)  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstTemp!= null)  
         {  
            PrintProviderError(rstTemp.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
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
// EndSupportsJ  
  
```  
  
## See Also  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)   
 [Supports Method](../../../ado/reference/ado-api/supports-method.md)