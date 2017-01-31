---
$title: "InternetTimeout Property Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "InternetTimeout property [ADO], VJ++ example"
ms.assetid: 757adb1b-d184-4887-bbe2-0f1bb6433f69
caps.latest.revision: 14
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# InternetTimeout Property Example (VJ++)
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/en-us/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](http://go.microsoft.com/fwlink/?LinkId=199565).  
  
 This example demonstrates the [InternetTimeout](../../../ado/reference/rds-api/internettimeout-property-rds.md) property, which exists on the [DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) and [DataSpace](../../../ado/reference/rds-api/dataspace-object-rds.md) objects. In this case, the **InternetTimout** property is demonstrated on the **DataControl** object and the timeout is set to 20 seconds.  
  
```  
// BeginInternetTimeoutJ  
// The WFC class includes the ADO objects.  
import com.ms.wfc.data.*;  
import com.ms.wfc.data.rds.*;  
import java.io.* ;  
  
public class InternetTimeoutX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      InternetTimeoutX();  
      System.exit(0);  
   }  
  
   // InternetTimeoutX function  
  
   static void InternetTimeoutX()  
   {  
  
      // Define ADO Objects.  
      Recordset rstAuthors = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      int intCount = 0;  
      int intDisplaysize = 15;  
  
      try  
      {  
         IBindMgr dc = (IBindMgr) new DataControl();  
         dc.setServer("http://MyServer");  
         dc.setConnect("DSN=pubs");  
         dc.setSQL("SELECT * FROM Authors");  
         dc.setInternetTimeout(20000);   // Wait at least 20 seconds.  
         dc.Refresh();  
         rstAuthors = (Recordset)dc.getRecordset();  
         while(!rstAuthors.getEOF())  
         {  
            System.out.println(rstAuthors.getField  
               ("au_fname").getString() + " " +   
               rstAuthors.getField("au_lname").getString());  
            intCount++;  
            if(intCount % intDisplaysize == 0)  
            {  
               System.out.println("\nPress <Enter> to continue..");  
               in.readLine();  
               intCount = 0;  
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
      catch(java.lang.UnsatisfiedLinkError e)  
      {  
         System.out.println("Exception: " + e.getMessage());  
      }  
      catch(java.lang.NullPointerException ne)  
      {  
         System.out.println(  
         "Exception: Attempt to use null where an object is required.");  
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
// EndInternetTimeoutJ  
```  
  
## See Also  
 [InternetTimeout Property (RDS)](../../../ado/reference/rds-api/internettimeout-property-rds.md)


