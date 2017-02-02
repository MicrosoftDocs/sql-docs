---
title: "CacheSize Property Example (VJ++) | Microsoft Docs"
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
  - "CacheSize property [ADO], VJ++ example"
ms.assetid: d6fe482a-6951-438b-be58-e08f64efd1e2
caps.latest.revision: 10
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# CacheSize Property Example (VJ++)
This example uses the [CacheSize](../../../ado/reference/ado-api/cachesize-property-ado.md) property to show the difference in performance for an operation performed with and without a 30-record cache.  
  
```  
// BeginCacheSizeJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class CacheSizeX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      CacheSizeX();  
      System.exit(0);  
   }  
  
   // CacheSizeX function  
  
   static void CacheSizeX()  
   {  
  
      // Define ADO Objects.  
      Recordset rstRoySched = null;  
  
      // Declarations.  
      BufferedReader in =  
         new BufferedReader (new InputStreamReader(System.in));  
      String line = null;  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
         + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      long timeStart;  
      long timeEnd;  
      float timeNoCache;  
      float timeCache;  
      int   intLoop;  
      String strTemp;  
  
      try  
      {  
         // Open the RoySched table.  
         rstRoySched = new Recordset();  
         rstRoySched.open("roysched", strCnn,  
            AdoEnums.CursorType.FORWARDONLY,  
            AdoEnums.LockType.READONLY,  
            AdoEnums.CommandType.TABLE);  
  
         // Enumerate the Recordset object twice and record  
         // the elapsed time.  
  
         timeStart = System.currentTimeMillis();  
         for ( intLoop = 0; intLoop < 2; intLoop++)  
         {  
            rstRoySched.moveFirst();  
            while (!rstRoySched.getEOF())  
            {  
               // Execute a simple operation for the  
               // performance test.  
               strTemp = rstRoySched.getField("title_id").getString();  
               rstRoySched.moveNext();  
            }  
  
         }  
  
         timeEnd = System.currentTimeMillis();  
         timeNoCache =(float)(timeEnd - timeStart)/1000f;  
  
         // Cache records in groups of 30 records.  
         rstRoySched.moveFirst();  
         rstRoySched.setCacheSize(30);  
         timeStart = System.currentTimeMillis();  
  
         // Enumerate the Recordset object twice and record  
         // the elapsed time.  
         for ( intLoop = 0; intLoop < 2; intLoop++)  
         {  
            rstRoySched.moveFirst();  
            while (!rstRoySched.getEOF())  
            {  
               // Execute a simple operation for the  
               // performance test.  
  
               strTemp = rstRoySched.getField("title_id").getString();  
               rstRoySched.moveNext();  
            }  
  
         }  
  
         timeEnd = System.currentTimeMillis();  
         timeCache = (float)(timeEnd - timeStart)/1000f;  
  
         // Display performance results.  
         System.out.println("\nCaching Performance Results:");  
         System.out.println("\n\tNo Cache: " + timeNoCache + " seconds");  
         System.out.println("\n\t30-record cache: " + timeCache +  
            " seconds");  
         System.out.println("\n\nPress <Enter> to continue..");  
         in.readLine();  
  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // Check for null pointer for connection object.  
         if (rstRoySched.getActiveConnection()==null)  
            System.out.println("Exception: " + ae.getMessage());  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstRoySched != null)  
         {  
            PrintProviderError(rstRoySched.getActiveConnection());  
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
         if (rstRoySched != null)  
            if (rstRoySched.getState() == 1)  
               rstRoySched.close();  
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
  
// EndCacheSizeJ  
```  
  
## See Also  
 [CacheSize Property (ADO)](../../../ado/reference/ado-api/cachesize-property-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)