using MathNet.Numerics.LinearAlgebra;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using Azure.Core;
using ItmoPhysics.Data;
using ItmoPhysics.Servicies.MatrixInfoUpload.CurvesUploads;

namespace ItmoPhysics.Servicies.MatrixInfoUpload.CellsUploads
{
    public class CurvesUploadBuilder : ICurvesUploadBuilder
    {
        private List<int>? _filesIds;
        private int _cellCount;
        private List<string>? _xColumnNames;
        private List<string>? _yColumnNames;
        private readonly ApplicationContext _context;

        public CurvesUploadBuilder(ApplicationContext context)
        {
            _context = context;
        }

        public bool IsValid { get; set; } = false;

        public ICurvesUpload Build()
        {
            if (_filesIds is null) throw new InvalidOperationException();
            if (_xColumnNames is null) throw new InvalidOperationException();
            if (_yColumnNames is null) throw new InvalidOperationException();
            if (_cellCount <= 0) throw new InvalidOperationException();
            if (!IsValid) throw new InvalidOperationException();

            return new CurvesUpload(_filesIds, _cellCount, _xColumnNames, _yColumnNames, _context);
        }

        public void Validate()
        {
            if (_filesIds is null) throw new InvalidOperationException();
            if (_xColumnNames is null) throw new InvalidOperationException();
            if (_yColumnNames is null) throw new InvalidOperationException();
            if (_cellCount <= 0) throw new InvalidOperationException();

            if (_filesIds.Count == _cellCount && _xColumnNames.Count == _cellCount && _yColumnNames.Count == _cellCount)
            {
                IsValid = true;
            }
        }

        public void WithCellCount(int cellCount)
        {
            IsValid = false;
            _cellCount = cellCount;
        }

        public void WithFileIds(List<int> filesIds)
        {
            IsValid = false;
            _filesIds = filesIds;
        }

        public void WithXColumnNames(IEnumerable<string> xColumnNames)
        {
            IsValid = false;
            _xColumnNames = xColumnNames.ToList();
        }

        public void WithYColumnNames(IEnumerable<string> yColumnNames)
        {
            IsValid = false;
            _yColumnNames = yColumnNames.ToList();
        }
    }
}
