using BzKovSoft.ObjectSlicer;
using BzKovSoft.ObjectSlicer.Samples;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instence;

    public GameObject _katana;
    public GameObject _box;

    private Rigidbody _boxRigidbody;

    private GameObject _sliced;
    private Material[] _materials;
    private List<GameObject> _slices = new List<GameObject>();

    private Vector3 _katanaPosition;
    private Vector3 _boxPosition;

    private bool _cantSlice = false;
    private bool _slicingProgres = false;
    private bool _slicingFinish = false;
    private bool _readyForNewSlice = true;
    private bool _levelFinish = false;

    private float _pointY;
    private float _mixingFactor;
    private float _widthOfSlice = 0;
    private float _previousWidthOfSlice = 0f;



    private bool _clickState = false;

    private void Start()
    {
        _katanaPosition = _katana.transform.position;
        _boxPosition = _box.transform.position;
        _boxRigidbody = _box.GetComponent<Rigidbody>();
        _mixingFactor = Mathf.Abs(_boxPosition.x);
        _pointY = _katanaPosition.y;
    }

    

    public GamePlayController()
    {
        instence = this;
    }

    public void Cut(GameObject target)
    {
        if (_cantSlice)
            return;

        var sliceadle = target.GetComponent<IBzSliceable>();

        if (sliceadle == null)
        {
            return;
        }


        Plane plane = new Plane(Vector3.right, 0);

        sliceadle.Slice(plane, r =>
        {
            if (!r.sliced)
            {
                return;
            }

            _slicingProgres = true;
            _slicingFinish = false;
            _readyForNewSlice = false;
            _sliced = r.outObjectPos;
            AddSlices(_sliced);
            _sliced.GetComponent<Rigidbody>().isKinematic = true;
            var meshFilter = _sliced.GetComponent<MeshFilter>();
            float centerX = meshFilter.sharedMesh.bounds.center.x;

            _materials = _sliced.GetComponent<MeshRenderer>().materials;
            foreach (var material in _materials)
            {
                material.SetFloat("_PointX", centerX);
            }

        });

    }

    public void AddSlices(GameObject slices)
    {
        _slices.Add(slices);
    }

    private void FixedUpdate()
    {
        MoveBox();
        MoveKatana();
        Control();
        CheckLevelFinish();
    }

    public float PersentSliced(out bool canUpdate)
    {
        if(_previousWidthOfSlice == _widthOfSlice)
        {
            canUpdate = false;
            return 0;
        }
        _previousWidthOfSlice = _widthOfSlice;
        canUpdate = true;
        return (_widthOfSlice / (_mixingFactor * 2)) * 100;
    }

    private void CheckLevelFinish()
    {
        if(_box.transform.position.x >= _mixingFactor)
        {
            _levelFinish = true;
        }
    }

    public int GetData(out bool levelFinish)
    {
        if (_levelFinish)
        {
            if(GamePrefabScript.instence != null)
               GamePrefabScript.instence.KillGamePrefab();
            levelFinish = true;
            return _slices.Count;
        }

        levelFinish = false;
        return 0;
    }

    private void Control()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _clickState = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _clickState = false;
            }
        }

        if(Input.GetMouseButton(0))
            _clickState = true;
        else if(Input.GetMouseButtonUp(0))
            _clickState= false;
        else
            _clickState = false;
    }

    private void MoveBox()
    {
        
        if (!_slicingProgres && _readyForNewSlice)
        {
            float slowFactor = 0.1f;
            _boxRigidbody.isKinematic = false;
            _boxRigidbody.velocity = _box.transform.right * slowFactor;
            return;
        }

        _boxRigidbody.isKinematic = true;

        Invoke("ChangeRadius", 0.05f);

    }

    private void ChangeRadius()
    {
        if (_readyForNewSlice)
            return;

        _widthOfSlice = ((_box.transform.position.x + _mixingFactor) - (_boxPosition.x + _mixingFactor));
        float radius;

        if (_widthOfSlice < 0.1f)
            radius = 0.2f;
        else if (_widthOfSlice > 0.1f && _widthOfSlice < 0.3f)
            radius = 0.6f;
        else if (_widthOfSlice > 0.3f && _widthOfSlice < 0.7f)
            radius = 0.8f;
        else
            radius = 1f;

            foreach (var material in _materials)
        {
            material.SetFloat("_Radius", radius);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        _slicingProgres = false;
        _slicingFinish = true;
        _cantSlice = true;
    }

    private void MoveKatana()
    {
        Vector3 direction = new Vector3(0, 0, 0);

        if (_clickState)
        {
            direction = new Vector3(0, -2, 0);
        }

        if (_slicingFinish)
        {
            FinishSclicing();
            if(!_readyForNewSlice)
            {
                ReturnKnife();
            }
        }
        else
        {
            if (_clickState)
                _katana.transform.Translate(direction * Time.deltaTime);
            else
                direction = new Vector3(0, 0, 0);
        }

        if (!_slicingProgres)
        {
            return;
        }

        Cutting();
    }

    private void Cutting()
    {
        float pointY = _sliced.transform.InverseTransformPoint(_katana.transform.position).y;

        if(pointY < _pointY)
        {
            _pointY = pointY;
        }

        foreach (var material in _materials)
        {
            material.SetFloat("_PointY", _pointY);
        }
    }

    private void FinishSclicing()
    {
        if (_sliced != null)
        {
            _sliced.GetComponent<Rigidbody>().isKinematic = false;
            _sliced.GetComponent<Rigidbody>().useGravity = true;
        }
        _pointY = _katanaPosition.y;
    }

    private void ReturnKnife()
    {
        if (_katana.transform.position == _katanaPosition)
        {
            _boxPosition = _box.transform.position;
            _readyForNewSlice = true;
            _slicingFinish = false;
            _clickState = false;
            _cantSlice = false;
            return;
        }           

        _katana.transform.position = Vector3.Lerp(_katana.transform.position, _katanaPosition, Time.deltaTime * 15f);
    }
}
