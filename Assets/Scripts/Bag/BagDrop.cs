using ThisGame.GeneralUI;
using ThisGame.Items;
using ThisGame.Utils;

namespace ThisGame.Bag {
    public class BagDrop : DroppableUI {
        public override void OnAfterDrop(DragDropUI scrp) {
            base.OnAfterDrop(scrp);
            var desc = scrp.GetComponent<ItemDescHolder>().Description;
            BagManager.Instance.InBag.EaddNset(desc, 1);
            Destroy(scrp.gameObject);
        }
    }
}
