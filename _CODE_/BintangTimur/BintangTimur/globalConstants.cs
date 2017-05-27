﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaSoft
{
    public static class globalConstants
    {
        // module ID to be used in the application       
        public const int TAMBAH_HAPUS_GROUP_USER = 2;
        public const int TAMBAH_HAPUS_GROUP_PELANGGAN = 3;

        public const int PENGATURAN_GRUP_AKSES = 51;
        public const int PENGATURAN_POTONGAN_HARGA = 52;
        public const int PENGATURAN_HARGA_JUAL = 53;
        public const int PENGATURAN_LIMIT_STOK = 54;
        public const int PENGATURAN_KATEGORI_PRODUK = 55;
        public const int PENGATURAN_NOMOR_RAK = 56;
        public const int CEK_DATA_MUTASI = 57;
        public const int TAMBAH_HAPUS_JURNAL_HARIAN = 58;
        public const int TAMBAH_HAPUS_USER = 59;
        public const int PERMINTAAN_MUTASI_BARANG = 60;
        public const int MUTASI_BARANG = 61;
        public const int PENERIMAAN_BARANG_DARI_MUTASI = 62;
        public const int PENERIMAAN_BARANG_DARI_PO = 63;
        public const int PURCHASE_ORDER_DARI_RO = 64;
        public const int RETUR_PEMBELIAN_KE_SUPPLIER = 65;
        public const int RETUR_PEMBELIAN_KE_PUSAT = 66;

        public const int STOK_PECAH_BARANG = 101;
        public const int PENYESUAIAN_STOK = 102;
        public const int PERMINTAAN_BARANG = 103;
        public const int REPRINT_PERMINTAAN_BARANG = 104;
        public const int PENERIMAAN_BARANG = 105;
        public const int PEMBAYARAN_HUTANG = 106;
        public const int PEMBAYARAN_PIUTANG = 107;
        public const int BROWSE_STOK_PECAH_BARANG = 108;
        public const int CHANGE_PASSWORD = 109;
        public const int LOGOUT_FORM = 110;
        public const int PRODUK_DETAIL_FORM = 111;
        public const int RETUR_PEMBELIAN = 112;
        public const int RETUR_PENJUALAN = 113;
        public const int RETUR_PENJUALAN_STOCK_ADJUSTMENT = 114;
        public const int CASHIER_MODULE = 115;
        public const int DATA_PIUTANG_MUTASI = 116;
        public const int BROWSE_PO_PENERIMAAN = 117;
        public const int BROWSE_MUTASI_PENERIMAAN = 118;
        public const int REPRINT_PURCHASE_ORDER = 119;
        public const int DUMMY_TRANSACTION_TAX = 120;
        public const int SALES_QUOTATION = 121;
        public const int EDIT_SALES_QUOTATION = 122;
        public const int SALES_ORDER_REVISION = 123;
        public const int DELIVERY_ORDER = 124;
        public const int COPY_DELIVERY_ORDER = 125;

        public const int NEW_GROUP_USER = 201;
        public const int EDIT_GROUP_USER = 202;
        public const int NEW_USER = 203;
        public const int EDIT_USER = 204;
        public const int NEW_BRANCH = 205;
        public const int EDIT_BRANCH = 206;
        public const int NEW_CATEGORY = 207;
        public const int EDIT_CATEGORY = 208;
        public const int NEW_UNIT = 209;
        public const int EDIT_UNIT = 210;
        public const int NEW_CUSTOMER = 211;
        public const int EDIT_CUSTOMER = 212;
        public const int NEW_SUPPLIER = 213;
        public const int EDIT_SUPPLIER = 214;
        public const int NEW_PRODUK = 215;
        public const int EDIT_PRODUK = 216;
        public const int NEW_REQUEST_ORDER = 217;
        public const int EDIT_REQUEST_ORDER = 218;
        public const int NEW_PRODUCT_MUTATION = 219;
        public const int VIEW_PRODUCT_MUTATION = 220;
        public const int REJECT_PRODUCT_MUTATION = 221;
        public const int NEW_PURCHASE_ORDER = 222;
        public const int EDIT_PURCHASE_ORDER = 223;
        public const int PRINTOUT_PURCHASE_ORDER = 224;
        public const int NEW_RETUR_PEMBELIAN = 225;
        public const int EDIT_RETUR_PEMBELIAN = 226;
        public const int NEW_REGION = 227;
        public const int EDIT_REGION = 228;
        public const int NEW_LOCATION = 229;
        public const int EDIT_LOCATION = 230;

        public const int NEW_AKUN = 501;  //start from 5
        public const int EDIT_AKUN = 502;
        public const int NEW_DJ = 503;
        public const int EDIT_DJ = 504;
        public const int SALES_COMMISSION = 505;
        public const int MEMBERSHIP_POINT = 506;
        public const int PRE_ORDER_SALES = 506;
        public const int PRE_ORDER_SALES_REVISION = 507;
        public const int DELETE_DJ = 508;
        public const int NEW_SALESPERSON = 509;
        public const int EDIT_SALESPERSON = 510;


        // THESE CONSTANTS ARE USED TO CHECK GROUP ACCESS MODULE
        // THE VALUES MUST BE TIED TO THE VALUES INSIDE THE DATABASE TABLE

        // MAIN_MENU MANAJEMEN
        public const int MENU_MANAJEMEN_SISTEM = 1;
        public const int MENU_DATABASE = 2;
        public const int MENU_MANAJEMEN_USER = 3;
        public const int MENU_MANAJEMEN_CABANG = 4;
        public const int MENU_SINKRONISASI_INFORMASI = 5;
        public const int MENU_PENGATURAN_PRINTER = 6;
        public const int MENU_PENGATURAN_GAMBAR_LATAR = 7;

        // MAIN MENU GUDANG
        public const int MENU_GUDANG = 101;
        public const int MENU_PRODUK = 102;
        public const int MENU_TAMBAH_PRODUK = 103;
        public const int MENU_PENGATURAN_HARGA= 104;
        public const int MENU_PENGATURAN_LIMIT_STOK= 105;
        public const int MENU_PENGATURAN_KATEGORI_PRODUK = 106;
        public const int MENU_PECAH_SATUAN_PRODUK= 107;
        public const int MENU_PENGATURAN_NOMOR_RAK = 108;
        public const int MENU_KATEGORI = 109;
        public const int MENU_SATUAN = 110;
        public const int MENU_TAMBAH_SATUAN = 111;
        public const int MENU_PENGATURAN_KONVERSI = 112;
        public const int MENU_STOK_OPNAME = 113;
        public const int MENU_PENYESUAIAN_STOK = 114;
        public const int MENU_MUTASI_BARANG = 115;
        public const int MENU_TAMBAH_MUTASI_BARANG= 116;
        public const int MENU_CEK_PERMINTAAN_BARANG = 117;
        public const int MENU_PENERIMAAN_BARANG = 118;
        public const int MENU_PENERIMAAN_BARANG_DARI_MUTASI = 119;
        public const int MENU_PENERIMAAN_BARANG_DARI_PO = 120;
        public const int MENU_STOK_TRANSFER = 121;
        public const int MENU_VIEW_HPP_PRODUCT = 122;

        // MAIN MENU PEMBELIAN
        public const int MENU_PEMBELIAN = 201;
        public const int MENU_SUPPLIER = 202;
        public const int MENU_REQUEST_ORDER = 203;
        public const int MENU_PURCHASE_ORDER = 204;
        public const int MENU_REPRINT_REQUEST_ORDER= 205;
        public const int MENU_RETUR_PEMBELIAN = 206;
        public const int MENU_RETUR_PERMINTAAN = 207;
        
        // MAIN MENU PENJUALAN
        public const int MENU_PENJUALAN = 301;
        public const int MENU_PELANGGAN = 302;

        public const int MENU_SALES_QUOTATION = 303;
        public const int APPROVAL_SALES_QUOTATION = 304;
        public const int MENU_PRE_ORDER_SALES = 305;
        public const int MENU_SALES_ORDER_REVISION = 306;
        public const int MENU_DELIVERY_ORDER = 307;

        public const int MENU_TRANSAKSI_PENJUALAN = 308;
        public const int MENU_SET_NO_FAKTUR = 309;
        public const int MENU_RETUR_PENJUALAN = 310;
        public const int MENU_RETUR_PENJUALAN_INVOICE = 311;
        public const int MENU_RETUR_PENJUALAN_STOK = 312;

        public const int MENU_COPY_DELIVERY_ORDER = 313;


        // MAIN MENU KEUANGAN
        public const int MENU_KEUANGAN = 401;
        public const int MENU_PENGATURAN_NO_AKUN = 402;
        public const int MENU_TRANSAKSI = 403;
        public const int MENU_TRANSAKSI_HARIAN = 404;
        public const int MENU_PEMBAYARAN_PIUTANG = 405;
        public const int MENU_PEMBAYARAN_PIUTANG_MUTASI = 406;
        public const int MENU_PEMBAYARAN_HUTANG_SUPPLIER = 407;
        public const int MENU_PENGATURAN_LIMIT_PAJAK = 408;

        public const int MENU_MODULE_MESSAGING = 501;
        public const int MENU_TAX_MODULE = 502;

        // MAIN MENU LAPORAN
        public const int MENU_LAPORAN = 800;
        public const int MENU_LAPORAN_PEMBELIAN_BARANG = 801;
        public const int MENU_LAPORAN_HUTANG_AKAN_JATUH_TEMPO = 802;
        public const int MENU_LAPORAN_HUTANG_LEWAT_JATUH_TEMPO = 803;
        public const int MENU_LAPORAN_PEMBAYARAN_HUTANG = 804;
        public const int MENU_LAPORAN_PENJUALAN_PRODUK = 805;
        public const int MENU_LAPORAN_OMZET_PENJUALAN = 806;
        public const int MENU_LAPORAN_TOP_SALE = 807;
        public const int MENU_LAPORAN_PENJUALAN_KASIR = 808;
        public const int MENU_LAPORAN_KEUANGAN = 809;
        public const int MENU_LAPORAN_PIUTANG_AKAN_JATUH_TEMPO = 810;
        public const int MENU_LAPORAN_PIUTANG_LEWAT_JATUH_TEMPO = 811;
        public const int MENU_LAPORAN_PEMBAYARAN_PIUTANG = 812;
        public const int MENU_LAPORAN_DEVIASI_STOK = 813;
        public const int MENU_LAPORAN_STOK_DIBAWAH_LIMIT = 814;
        public const int MENU_LAPORAN_RETUR_BARANG = 815;
        public const int MENU_LAPORAN_MUTASI_BARANG = 816;
        public const int MENU_LAPORAN_PEMBAYARAN_MUTASI = 817;
        


        // CONSTANTS FOR USER CHANGE LOG
        public const int CHANGE_LOG_LOGIN = 1;
        public const int CHANGE_LOG_LOGOUT = 2;
        public const int CHANGE_LOG_INSERT = 3;
        public const int CHANGE_LOG_UPDATE = 4;
        public const int CHANGE_LOG_SET_NON_ACTIVE = 5;
        public const int CHANGE_LOG_CASHIER_LOGIN = 6;
        public const int CHANGE_LOG_CASHIER_LOGOUT = 7;
        public const int CHANGE_LOG_PAYMENT_CREDIT = 8;
        public const int CHANGE_LOG_PAYMENT_DEBT = 9;


        //mode laporan
        public const int MENU_REPORT_USER = 601;

        public const int REPORT_SALES_SUMMARY = 701;
        public const int REPORT_SALES_DETAILED = 702;
        public const int REPORT_SALES_PRODUCT = 703;
        public const int REPORT_TOPSALES_byTAGS = 704;
        public const int REPORT_TOPSALES_byDATE = 705;
        public const int REPORT_TOPSALES_GLOBAL = 706;
        public const int REPORT_TOPSALES_ByMARGIN = 707;
        public const int REPORT_SALES_OMZET = 708;

        public const int REPORT_PURCHASE_SUMMARY = 710;
        public const int REPORT_PURCHASE_DETAILED = 711;
        public const int REPORT_PURCHASE_ByPRODUCT = 712;

        public const int REPORT_PURCHASE_RETURN = 720;
        public const int REPORT_SALES_RETURN = 721;
        public const int REPORT_REQUEST_RETURN = 722;
        public const int REPORT_PRODUCT_MUTATION = 723;
        public const int REPORT_STOCK_DEVIATION = 724;
        public const int REPORT_STOCK = 725;

        public const int REPORT_DEBT_PAYMENT = 731;
        public const int REPORT_CREDIT_PAYMENT = 732;
        public const int REPORT_MUTATION_PAYMENT = 733;

        public const int REPORT_FINANCE_IN = 741;
        public const int REPORT_FINANCE_OUT = 742;
        public const int REPORT_MARGIN = 743;
        public const int REPORT_MONTHLY_BALANCE = 744;

        public const int VIEW_SUPPLIER = 800;

        //XML file
        public const string AccountXML = "MasterAccount.xml";
        public const string BranchXML = "MasterBranch.xml";
        public const string CategoryXML = "MasterCategory.xml";
        public const string CustomerXML = "MasterCustomer.xml";
        public const string GroupXML = "MasterGroup.xml";
        public const string ProductXML = "MasterProduct.xml";
        public const string SupplierXML = "MasterSupplier.xml";
        public const string UnitXML = "MasterUnit.xml";
        public const string UserXML = "MasterUser.xml";
        public const string ProductCategoryXML = "ProductCategory.xml";
        public const string SalesReceiptXML = "SalesReceipt.xml";

        public const string SalesSummaryXML = "SalesSummary.xml";
        public const string SalesDetailedXML = "SalesDetailed.xml";
        public const string SalesbyProductXML = "SalesbyProduct.xml";
        public const string TopSalesGlobalXML = "TopSalesGlobal.xml";
        public const string TopSalesbyTagsXML = "TopSalesbyTags.xml";
        public const string TopSalesbyDateXML = "TopSalesbyDate.xml";
        public const string TopSalesbyMarginXML = "TopSalesbyMargin.xml";
        public const string SalesOmzetXML = "SalesOmzet.xml";
        public const string PrintBarcodeXML = "PrintBarcode.xml";
        public const string CashierLogXML = "CashierLog.xml";

        public const string ProductDeviationXML= "ProductDeviation.xml";
        public const string ProductStockLimitXML = "ProductStockLimit.xml";

        public const string ProductMutationXML = "ProductMutation.xml";
        public const string MutationPaidXML = "MutationPaid.xml";

        public const string FinanceInXML = "FinanceIn.xml";
        public const string FinanceOutXML = "FinanceOut.xml";
        public const string MarginXML = "Margin.xml";
        public const string MonthlyBalanceXML = "MonthlyBalance.xml";

        public const string StockXML = "Stock.xml";
        public const string SalesReturnXML = "SalesReturn.xml";
        public const string PurchaseReturnXML = "PurchaseReturn.xml";
        public const string RequestReturnXML = "RequestReturn.xml";

        public const string DebtUnpaidXML = "DebtUnpaid.xml";
        public const string DebtPaidXML = "DebtPaid.xml";
        public const string DebtDueXML = "DebtDue.xml";

        public const string CreditUnpaidXML = "CreditUnpaid.xml";
        public const string CreditPaidXML = "CreditPaid.xml";
        public const string CreditDueXML = "CreditDue.xml";

        public const string PurchaseSummaryXML = "PurchaseSummary.xml";
        public const string PurchaseDetailedXML = "PurchaseDetailed.xml";
        public const string PurchasebyProductXML = "PurchasebyProduct.xml";

        public const string purchaseOrderXML = "PurchaseOrder.xml";
        public const string penerimaanBarangXML = "ProductsReceived.xml";
        public const string returPermintaanXML = "ReturPermintaan.xml";
        public const string deliveryOrderXML = "DeliveryOrder.xml";
        public const string creditPaymentXML = "creditPaymentPrintOut.xml";
        public const string returPenjualanXML = "dataReturPenjualanPrintOut.xml";
        public const string penjualanRegionXML = "penjualanByRegion.xml";

        public const string allSalesCommissionXML = "SalesCommission_ALL.xml";
        public const string salesCommissionXML = "SalesCommission.xml";
        public const string allMembershipPointXML = "MembershipPoint_ALL.xml";
        public const string membershipPointXML = "MembershipPoint.xml";

    }
}
