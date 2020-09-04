## [Drag Main window for VisualStudio](https://marketplace.visualstudio.com/items?itemName=gekka.DragVSMainWindow)

This extension can grab near title bar for drag move MainWindow with the Shift key + left mouse button.

After installation , you will get a performance warning.  
This is because synchronous loading of extensions is not allowed.

This extension needs to register event immediately after launched.
You can check "Tools->Environment->Extensions->Allow synchronous autoload of extensions"

---

VisualStudioのタイトルバー付近で左マウスボタンを使ってShiftキーを押しながらドラッグするとウィンドウを動かせます。

実行するとパフォーマンスの警告が出ます。 この拡張機能は起動直後にイベントの登録を行う必要があり、遅延読み込みでは間に合わないためです。 
「ツール->環境->拡張機能->拡張機能の同期読み込みを許可します」にチェックを入れると警告が出なくなります。
