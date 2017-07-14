using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TakePicture : MonoBehaviour {
    [SerializeField]
    GameObject TakePicOkayCanvas;
    [SerializeField]
    AudioClip Clip;
    string directory_path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    void Start()
    {
        //directory_path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        directory_path += "\\BoxShooting_BillLiao\\SavedScreen";
        if (Directory.Exists(directory_path) == false)
        {
            Directory.CreateDirectory(directory_path);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(!is_shooting)
                StartCoroutine(GetPNG());
        }
    }
    bool is_shooting = false;
    IEnumerator GetPNG()
    {
        is_shooting = true;
        //music
        gameObject.GetComponent<AudioSource>().clip = Clip;
        gameObject.GetComponent<AudioSource>().Play();
        // 因为"WaitForEndOfFrame"在OnGUI之后执行  
        // 所以我们只在渲染完成之后才读取屏幕上的画面  
        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        // 创建一个屏幕大小的纹理，RGB24 位格（24位格没有透明通道，32位的有）  
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        // 读取屏幕内容到我们自定义的纹理图片中  
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        // 保存前面对纹理的修改  
        tex.Apply();

        // 编码纹理为PNG格式  
        byte[] bytes = tex.EncodeToPNG();
        // 销毁吴永的图片纹理  
        Destroy(tex);
        DateTime current_datetime = System.DateTime.Now;
        string cs = current_datetime.ToString();
        cs = cs.Replace("\\", "_");
        cs = cs.Replace("/", "_");
        cs = cs.Replace(" ", "_");
        cs = cs.Replace(":", "_");
        // 将字节保存成图片，这个路径只能在PC端对图片进行读写操作
        File.WriteAllBytes(directory_path+"\\"+ cs+".png", bytes);
        // 这个路径会将图片保存到手机的沙盒中，这样就可以在手机上对其进行读写操作了  
        //File.WriteAllBytes(Application.persistentDataPath + "/onMobileSavedScreen.png", bytes);
        TakePicOkayCanvas.SetActive(true);
        yield return new WaitForSecondsRealtime(1.0f);
        TakePicOkayCanvas.SetActive(false);
        is_shooting = false;
    }
}
