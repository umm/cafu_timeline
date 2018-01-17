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

### View クラス作成

```csharp
using System;
using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Presentation.View;

namespace MainProject.SubProject.Presentation.View.SampleScene {

    public enum TimelineName {
        Hoge,
        Fuga,
        Piyo,
        Foo_Bar,
    }

    [Serializable]
    public class TimelineInformation : TimelineInformation<TimelineName> {}

    public class Timeline : TimelineView<TimelineName, TimelineInformation> {

        public override ITimelinePresenter<TEnum, TTimelineInformation> GetTimelinePresenter() {
            // IView に対する extension methods を生やしてある場合は return this.GetPresenter(); とかでもOK
            return SampleSceneViewController.Instance.Presenter;
        }

    }

}
```

#### TimelineView

* Hierarchy 上の GameObject にアタッチするための Component を作ります。
* 基底クラスとして `CAFU.Timeline.Presentation.View.TimelineView<TEnum, TTimelineInformation>` を作ってあるので、それを継承します。
* abstract メソッドとして `ITimelinePresenter<TEnum, TTimelineInformation> GetTimelinePresenter()` を要求されるので、 ITimelinePresenter を実装している Presenter のインスタンスを返します。
* ファイル名を `Timeline.cs` として保存します。
  * MonoBehaviour の制約として、クラス名とファイル名が同一である必要があるため。

#### enum TimelineName

* 操作対象の TimelineAsset 名を列挙した enum を定義します。
* この enum の名称をもとに、操作対象の PlayableDirector を解決します。

##### 解決ルール

1. Timeline コンポーネントの Timeline Information List に設定済の情報
1. Timeline GameObject 直下にある GameObject のうち、enum の名称と完全一致する要素
1. Timeline GameObject 以下にある GameObject のうち、enum の名称のアンダースコアを階層区切り（スラッシュに変換）と見なしてパスが一致する要素
1. Timeline GameObject 以下にある GameObject のうち、単一の GameObject の名称が enum の名称と完全一致する要素

##### 例

```
Controller
Timeline
  Toggle
    Show
      ShowHoge
      ShowFuga
    Hide
      HideHoge
      HideFuga
```

* `TimelineName.Toggle`: `/Timeline/Toggle`
* `TimelineName.Toggle_Show_ShowHoge`: `/Timeline/Toggle/Show/ShowHoge`
* `TimelineName.Hide`: `/Timeline/Toggle/Hide`

#### TimelineInformation

* `CAFU.Timeline.Domain.Model.TimelineInformation<TEnum>` クラスを拡張したクラスを作ります。
  * Unity の仕様として、Generics なクラスを Serialize 出来ないため、冗長ですが仕方ありません 😓
* 当該ファイルに `System.Serializable` 属性を付けます。
* もし、Inspector 上でのエイリアス設定を必要としない（全ての Timeline に於いて enum の名称と Hierarchy 上のパスが一致している）場合は、このクラスを作る必要はありません。

### Presenter 実装

```csharp
using CAFU.Core.Domain;
using CAFU.Core.Presentation;
using CAFU.Timeline.Domain.UseCase;
using CAFU.Timeline.Presentation.Presenter;
using MainProject.SubProject.Presentation.View.SampleScene;

namespace MainProject.SubProject.Presentation.Presenter {

    public class SamplePresenter : ITimelinePresenter<TimelineName, TimelineInformation>, IPresenterBuilder {

        public TimelineUseCase<TimelineName, TimelineInformation> TimelineUseCase { get; private set; }

        public void Build() {
            this.TimelineUseCase = UseCaseFactory.CreateInstance<TimelineUseCase<TimelineName, TimelineInformation>>();
        }

    }

}
```

* 任意の Presenter で `ITimelinePresenter<TEnum, TTimelineInformation>` を実装します。
  * 必須プロパティとして `public TimelineUseCase<TimelineName> TimelineUseCase { get; }` を実装します。
* `Build()` メソッド内で初期化すると良いでしょう。

### GameObject にアタッチ

* Hierarchy のルート階層に `Timeline` GameObject を作成し、上記で作った View クラスを AddComponent します。

### PlayableDirector の登録

* `Timeline` GameObject の子要素として、シーン内で再生する Timeline (PlayableDirector) を複数登録します。

## Timeline 再生・停止など

* Presenter の拡張メソッドとして `PlayableDirector GetPlayableDirector(TEnum)` が生やしてあるので、取得した [`PlayableDirector`](https://docs.unity3d.com/ScriptReference/Playables.PlayableDirector.html) のメソッドを叩いてください。
* 基本的には enum の値をもとに、Hierarchy の `Timeline/` 以下の GameObject を探しに行きます。
  * enum と Hierarchy 上のパスが異なる場合は、 Timeline コンポーネントの Timeline Information List に対して手動で設定することも可能です。
  * Hierarchy 的にネストしている場合は、enum の名称をアンダースコアで区切ると、それを階層の区切りと見なして（スラッシュに変換して）探しに行きます。

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


