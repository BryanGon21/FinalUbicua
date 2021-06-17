import 'package:flutter/material.dart';
import 'package:tts_azure/tts_azure.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:intl/intl.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'TTSAzure',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(title: 'TTSAzure'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key key, this.title}) : super(key: key);
  final String title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  TTSAzure _ttsazure;
  TextEditingController _controller;
  String _lang = 'es-ES';
  String _shortName = 'es-ES-Pablo-Apollo';


Future<String> parsedthetext()async{
    var responset = await http.post(
      Uri.encodeFull("https://apiproductorfinal.azurewebsites.net/api/mudo"),
      headers: {"Content-Type": "application/json"},
      body: jsonEncode(<String, String>{
        "NameDevice":"Xiaomi",
        "Date": DateFormat('yyyy-MM-ddTkk:ss').format(DateTime.now()).toString(),
        "Text": _controller.text
      })
    );
    print(responset.body);
    return responset.body;
    
  }

  
  @override
  void initState() {
    _ttsazure = TTSAzure("023b9aaeb4cc4da49950a8c386c8c4be", "eastus");
    _controller = TextEditingController();
    _controller.text = "¡Hola Mundo!";
    super.initState();
  }

  void _play() {

    _ttsazure.speak(_controller.text, _lang, _shortName);
    
  

  }

  void _stop() {
    _ttsazure.stop();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          children: <Widget>[
            ListTile(
              title: TextField(
                maxLines: 10,
                controller: _controller,
              ),
            )
          ],
        ),
      ),
      floatingActionButton: Row(
        mainAxisAlignment: MainAxisAlignment.end,
        children: <Widget>[
          FloatingActionButton(
            onPressed: _play, 
            tooltip: 'Speech to Text',
            child: Icon(Icons.play_arrow),
          ),
          SizedBox(width: 15,),
          FloatingActionButton(
            onPressed: () => parsedthetext(),
            tooltip: 'Stop speech',
            child: Icon(Icons.stop),
          )
        ],
      ),
    );
  }
}
