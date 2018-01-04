# What

* Timeline に関する処理を扱う CAFU UseCase

# Requirement

* Unity 2017.3
  * .NET 4.6 (Experimental)
* [CAFU](https://github.com/umm-projects/cafu_core)

# Install

```shell
$ npm install github:umm-projects/cafu_timeline
```

# Usage

## 事前準備

### enum TimelineName 定義

* 操作対象の TimelineAsset 名を列挙した enum を定義します。
* namespace は何処でも良いですが、 View の下が実装上楽になると思います。

```csharp
namespace MainProject.SubProject.Presentation.View.SampleScene {

    public enum TimelineName {
        Hoge,
        Fuga,
        Piyo,
    }

}
```

### TimelineInformation クラス拡張

* `CAFU.Timeline.Domain.Model.TimelineInformation<TEnum>` クラスを拡張したクラスを作ります。
  * Unity の仕様として、Generics なクラスを Serialize 出来ないため、冗長ですが仕方ありません 😓
* namespace は何処でも良いですが、 enum と同じく View の下がヨサソウです。
  * enum と同ファイルに定義すると良いでしょう。
* 当該ファイルに `System.Serializable` 属性を付けます。

```csharp
using System;
using CAFU.Timeline.Domain.Model;

namespace MainProject.SubProject.Presentation.View.SampleScene {

    [Serializable]
    public class TimelineInformation : TimelineInformation<TimelineName> {}

    public enum TimelineName {
        Hoge,
        Fuga,
        Piyo,
    }

}
```

### Presenter 実装

* 任意の Presenter で `ITimelinePresenter<TEnum, TTimelineInformation>` を実装します。
  * 必須プロパティとして `public TimelineUseCase<TimelineName> TimelineUseCase { get; }` を実装します。
* `Build()` メソッド内で初期化すると良いでしょう。

### View クラス作成

* Hierarchy 上の GameObject にアタッチするための Component を作ります。
* 基底クラスとして `CAFU.Timeline.Presentation.View.TimelineView<TEnum, TTimelineInformation>` を作ってあるので、それを継承します。
* abstract メソッドとして `ITimelinePresenter<TEnum, TTimelineInformation> GetTimelinePresenter()` を要求されるので、 ITimelinePresenter を実装している Presenter のインスタンスを返します。

```csharp
using System;
using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Presentation.View;

namespace MainProject.SubProject.Presentation.View.SampleScene {

    [Serializable]
    public class TimelineInformation : TimelineInformation<TimelineName> {}

    public enum TimelineName {
        Hoge,
        Fuga,
        Piyo,
    }

    public class Timeline : TimelineView<TimelineName, TimelineInformation> {

        public override ITimelinePresenter<TEnum, TTimelineInformation> GetTimelinePresenter() {
            return SampleSceneViewController.Instance.Presenter;
        }

    }

}
```

* MonoBehaviour の制約として、クラス名とファイル名が同一である必要があるため、 `Timeline.cs` として保存します。

## Timeline 再生・停止など

* Presenter の拡張メソッドとして `PlayableDirector GetPlayableDirector(TEnum)` が生やしてあるので、取得した [`PlayableDirector`](https://docs.unity3d.com/ScriptReference/Playables.PlayableDirector.html) のメソッドを叩いてください。

## GenericBinding の設定

* 実行時の Timeline 操作対象設定用に Presenter の拡張メソッドとして `void SetGenericBindingBy***()` が生やしてあります。
  * 設定すべき PlayableAsset の検出のために、 TrackName, PlayableAssetName, TrackNameAndPlayableAssetName の3種類のメソッドを用意しています。
* 設定する値の型に厳密なので、以下の対応に従って設定してください。

| TrackAsset | 型 |
| --- | --- |
| AnimationTrack | Animator |
| AudioTrack | AudioSource |
| ActivationTrack | GameObject |

```csharp
SampleViewController.Instance.Presenter.SetGenericBindingByPlayableAssetName(TimelineName.Hoge, "FadeIn", this.GetComponent<Animator>());
```

## ReferenceValue の設定

* 実行時の ExposedReference 参照値設定用に Presenter の拡張メソッドとして `void SetReferenceValueByPlayableAssetName()` が生やしてあります。
* 設定する値の型に厳密なので、以下の対応に従って設定してください。

| PlayableAsset | 型 |
| --- | --- |
| ControlPlayableAsset | GameObject |

```csharp
SampleViewController.Instance.Presenter.SetReferenceValueByPlayableAssetName(TimelineName.Hoge, "SomeReference", this.gameObject);
```

# License

Copyright (c) 2017 Tetsuya Mori

Released under the MIT license, see [LICENSE.txt](LICENSE.txt)


