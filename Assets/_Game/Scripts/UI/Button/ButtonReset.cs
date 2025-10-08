public class ButtonReset : ButtonConfirmationTemplate
{
    public override void OnButtonClick()
    {
        base.OnButtonClick();

        CoreManager.Instance.panelConfirmation.SetAction(() =>
        {
            SaveManager.Instance.ResetData();
            CoreManager.Instance.ShowNotif("Berhasil menghapus data");
        });
    }
}
