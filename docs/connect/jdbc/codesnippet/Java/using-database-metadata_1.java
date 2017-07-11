public static void getDatabaseMetaData(Connection con) {
   try {
      DatabaseMetaData dbmd = con.getMetaData();
      System.out.println("dbmd:driver version = " + dbmd.getDriverVersion());
      System.out.println("dbmd:driver name = " + dbmd.getDriverName());
      System.out.println("db name = " + dbmd.getDatabaseProductName());
      System.out.println("db ver = " + dbmd.getDatabaseProductVersion());
   }
   catch (Exception e) {
      e.printStackTrace();
   }
}