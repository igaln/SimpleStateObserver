# SimpleStateObserver
Basic Observable Pattern in Unity

1. SBObservable<T> - Generic Typed observable object using a action callback when data is changed
2. ObservableList<T> - Generic Typed observable list using action callbacks when list is changed.

### Usage
#### Create an object with a Type
        public SBObservable<Texture2D> store = new SBObservable<Texture2D>();
#### Bind Data to a callback
        model.dataUpdated += (T obj) =>
        {
            //Ugly type casting, know a better way to do this?
            Texture2D newT2 = (Texture2D)(object)obj;
            Image.sprite = Sprite.Create(newT2, new Rect(0.0f, 0.0f, newT2.width, newT2.height), new Vector2(0.5f, 0.5f), 100.0f);
        };
#### Update data in Observer to trigger callback
      store.Data = texture;
 
