---
title: "NumericScale and Precision Properties of Column Example (VC++)"
description: "NumericScale and Precision Properties of the Column Object Example (VC++)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Precision property [ADOX], VC++ example"
  - "NumericScale property [ADOX], VC++ example"
dev_langs:
  - "C++"
---
# NumericScale and Precision Properties of the Column Object Example (VC++)
This example demonstrates the [NumericScale](./numericscale-property-adox.md) and [Precision](./precision-property-adox.md) properties of the [Column](./column-object-adox.md) object. This code displays their value for the **Order Details** table of the *Northwind* database.  
  
```  
// BeginNumericScalePrecCpp.cpp  
// compile with: /EHsc  
#import "msado15.dll" rename("EOF", "EndOfFile")  
#import "msadox.dll" no_namespace  
  
#include "iostream"  
using namespace std;  
  
inline void TESTHR(HRESULT x) {if FAILED(x) _com_issue_error(x);};  
  
int main() {  
   if ( FAILED(::CoInitialize(NULL)) )  
      return -1;  
  
   HRESULT hr = S_OK;  
  
   // Define and initialize ADOX object pointers. These are in ADODB namespace.  
   _CatalogPtr m_pCatalog = NULL;  
   _TablePtr m_pTable = NULL;  
   _ColumnPtr m_pColumn = NULL;  
  
   // Define ADODB object pointers  
   ADODB::_ConnectionPtr m_pCnn = NULL;  
  
   // Declare string variables  
   _bstr_t strCnn("Provider='Microsoft.JET.OLEDB.4.0';Data Source='c:\\Northwind.mdb';");  
  
   try {  
      TESTHR(hr = m_pCnn.CreateInstance(__uuidof(ADODB::Connection)));  
  
      TESTHR(hr = m_pCatalog.CreateInstance(__uuidof (Catalog)));  
  
      // Connect the catalog.  
      m_pCnn->Open (strCnn, "", "", NULL);  
  
      m_pCatalog->PutActiveConnection(variant_t((IDispatch *)m_pCnn));  
  
      // Retrieve the Order Details table  
      m_pTable = m_pCatalog->Tables->GetItem("Order Details");  
  
      // Display numeric scale and precision of small integer fields.  
      _variant_t vIndex;  
      for ( long lIndex = 0 ; lIndex < m_pTable->Columns->Count ; lIndex++ ) {  
         vIndex = lIndex ;  
         m_pColumn = m_pTable->Columns->GetItem(vIndex);  
         if (m_pColumn->Type == adSmallInt) {  
            cout << "Column: " << m_pColumn->GetName() << endl;  
            cout << "Numeric scale: " << (_bstr_t) m_pColumn->GetNumericScale() << endl;  
            cout << "Precision: " << m_pColumn->GetPrecision() << endl;  
         }  
      }  
   }  
   catch(_com_error &e) {  
      // Notify the user of errors if any.  
      _bstr_t bstrSource(e.Source());  
      _bstr_t bstrDescription(e.Description());  
  
      printf("\n\tSource :  %s \n\tdescription : %s \n ", (LPCSTR)bstrSource, (LPCSTR)bstrDescription);  
   }  
   catch(...) {  
      cout << "Error occurred in NumericScalePrecX...." << endl;  
   }  
   ::CoUninitialize();  
}  
```