//最小
//Run(() => Window(),"NXUI", args);

//サンプルワンライナー
//Run(() => Window().Content(Label().Content("NXUI")),"NXUI", args);

//sample memo app
Run(
  () => Window()
    .Width(500).Height(300).Title("NXUI memo app sample")
    .Content(
      StackPanel()
        .Children(
          TextBox(out var tbox)
            .MinHeight(200)
            .AcceptsReturn(true)
            .Watermark("文字入力ボックス"),
          Button()
            .Content("ボタン")
            .OnClickHandler( async (b, a) => {
              var top = TopLevel.GetTopLevel(b);
              if (top is null) return;
              var file = await top
                .StorageProvider
                .SaveFilePickerAsync(new() {Title = "テキストの保存"});
              if(file is not null){
                await using var str = await file.OpenWriteAsync();
                using var stw = new StreamWriter(str);
                await stw.WriteAsync(tbox.Text);
              }
            })
        )
      ),
  "memo app", args);

//sample memo app with Rx
/*
Run(
  () => Window()
    .Width(500).Height(300).Title("NXUI memo app sample")
    .Content(
      StackPanel()
        .Children(
          TextBox(out var tbox)
            .MinHeight(200)
            .AcceptsReturn(true)
            .Watermark("文字入力ボックス"),
          Button(out var button)
            .Content("ボタン")
            .OnClick((_, o) => o.Subscribe( async _ => {
              var top = TopLevel.GetTopLevel(button);
              if (top is null) return;
              var file = await top
                .StorageProvider
                .SaveFilePickerAsync(new() {Title = "テキストの保存"});
              if(file is not null){
                await using var str = await file.OpenWriteAsync();
                using var stw = new StreamWriter(str);
                await stw.WriteAsync(tbox.Text);
              }
            }))
        )
      ),
  "memo app", args);
*/