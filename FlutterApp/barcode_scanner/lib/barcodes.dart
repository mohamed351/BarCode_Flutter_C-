import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:flutter_barcode_scanner/flutter_barcode_scanner.dart';

class BarCodes extends StatefulWidget {
  static const router = "/barcodes";
  const BarCodes({Key? key}) : super(key: key);

  @override
  State<BarCodes> createState() => _BarCodesState();
}

class _BarCodesState extends State<BarCodes> {
  late Socket _socket;
  @override
  Widget build(BuildContext context) {
    final args =
        ModalRoute.of(context)!.settings.arguments as Map<String, String>;
    print(args["Socket"]);

    Future<void> connectSocket() async {
      final url = args["Socket"]!.split(":");
      print(url);
      _socket = await Socket.connect(
          url[0],
          int.parse(url[1], onError: (source) {
            return 1;
          }));
    }

    return Scaffold(
      appBar: AppBar(title: Text(args["Socket"].toString())),
      floatingActionButton: FloatingActionButton(onPressed: () async {
        await scanBarcodeNormal();
      }),
      body: FutureBuilder(
        future: connectSocket(),
        builder: ((context, snapshot) => Container(
                child: ListView(
              children: [
                ListTile(
                  title: Text(""),
                )
              ],
            ))),
      ),
    );
  }

  Future<void> scanBarcodeNormal() async {
    String barcodeScanRes;
    // Platform messages may fail, so we use a try/catch PlatformException.
    try {
      barcodeScanRes = await FlutterBarcodeScanner.scanBarcode(
          '#ff6666', 'Cancel', true, ScanMode.BARCODE);
      _socket.write(barcodeScanRes);
    } on PlatformException {
      barcodeScanRes = 'Failed to get platform version.';
    }

    // If the widget was removed from the tree while the asynchronous platform
    // message was in flight, we want to discard the reply rather than calling
    // setState to update our non-existent appearance.
  }
}
