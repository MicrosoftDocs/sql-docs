---
title: "Clone Method Example (VJ++) | Microsoft Docs"
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
  - "Clone method [ADO], VJ++ example"
ms.assetid: 6b699f2b-e5c7-4584-ab25-663a9243d30e
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "jhubbard"
---
# Clone Method Example (VJ++)
This example uses the [Clone](../../../ado/reference/ado-api/clone-method-ado.md) method to create copies of a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) and then lets the user position the record pointer of each copy independently.  
  
```  
// BeginCloneJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class CloneX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      CloneX();  
      System.exit(0);  
   }  
  
   // CloneX function  
  
   static void CloneX()  
   {  
      // Assign SQL statement and connection string to variables.  
      String strSQL = "SELECT stor_name FROM Stores "  
         + "ORDER BY stor_name";  
  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
         + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      // Define ADO Objects.  
      Recordset[] arstStores = null;  
  
      //Declarations.  
      BufferedReader in =  
         new BufferedReader (new InputStreamReader(System.in));  
      String line = null;  
      String strMessage;  
      String strFind;  
      int  intLoop;  
      boolean booExit = true;  
  
      try  
      {  
         // Open recordset as a static cursor type recordset.  
         arstStores = new Recordset[3];  
         arstStores[0] = new Recordset();  
         arstStores[0].setCursorType(AdoEnums.CursorType.STATIC);  
         arstStores[0].setLockType(AdoEnums.LockType.BATCHOPTIMISTIC);  
         arstStores[0].open(strSQL,strCnn,AdoEnums.CursorType.STATIC,  
            AdoEnums.LockType.BATCHOPTIMISTIC,AdoEnums.CommandType.TEXT);  
  
         // Create two clones of the original Recordset.  
         arstStores[1] = (Recordset)arstStores[0].clone  
            (AdoEnums.LockType.BATCHOPTIMISTIC);  
         arstStores[2] = (Recordset)arstStores[0].clone  
            (AdoEnums.LockType.BATCHOPTIMISTIC);  
  
         while(booExit)  
         {  
            // Loop through the array so that on each pass,  
            // the user is searching a different copy of the  
            // same Recordset.  
            for (intLoop = 0; intLoop < 3; intLoop++)  
            {  
               // Ask for search string while showing where  
               // the current record pointer is for each Recordset  
               strMessage = "\nRecordsets from stores table:" + "\n"  
                  +  "  1 - Original - Record pointer at "  
                  + arstStores[0].getField("stor_name").getString()  
                  + "\n" + "  2 - Clone - Record pointer at "  
                  + arstStores[1].getField("stor_name").getString()  
                  + "\n" + "  3 - Clone - Record pointer at "  
                  + arstStores[2].getField("stor_name").getString()  
                  + "\n";  
               System.out.println(strMessage);  
               System.out.println("Enter search string for #"  
                  + (intLoop+1) + "(Press <Enter> to Exit.)");  
  
               strFind = in.readLine().trim();  
               if(strFind.length() == 0)  
               {  
                  booExit = false;  
                  break;  
               }  
  
               // Find the search string; if there's no  
               // match, jump to the last record.  
               arstStores[intLoop].setFilter("stor_name >= '" +  
                  strFind + "'");  
               if (arstStores[intLoop].getEOF())  
               {  
                  arstStores[intLoop].setFilter  
                     (new Integer(AdoEnums.FilterGroup.NONE));  
                  arstStores[intLoop].moveLast();  
               }  
            }  
         }  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a connection, check for null pointer first.  
         if (arstStores[0] != null)  
         {  
            PrintProviderError(arstStores[0].getActiveConnection());  
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
         if (arstStores[0] != null)  
            if (arstStores[0].getState() == 1)  
               arstStores[0].close();    
         if (arstStores[1] != null)  
            if (arstStores[1].getState() == 1)  
               arstStores[1].close();    
         if (arstStores[2] != null)  
            if (arstStores[2].getState() == 1)  
               arstStores[2].close();  
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
// EndCloneJ  
```  
  
## See Also  
 [Clone Method (ADO)](../../../ado/reference/ado-api/clone-method-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)