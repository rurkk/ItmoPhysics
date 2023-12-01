using Azure.Core;
using ItmoPhysics.Data;
using ItmoPhysics.Servicies.MatrixInfoUpload.CurvesUploads;
using System;
using System.Security.Claims;
using WebMatrixUploader.Data.DataForMatrix;

namespace ItmoPhysics.Servicies.MatrixInfoUpload.CellsUploads
{
    public class CurvesUpload : ICurvesUpload
    {
        public List<int> _matrixId;
        public int _cellCount;
        public List<string> _xColumnNames;
        public List<string> _yColumnNames;
        private readonly ApplicationContext _context;

        public CurvesUpload(List<int> matrixId, int cellCount, List<string> xColumnNames, List<string> yColumnNames, ApplicationContext context)
        {
            _matrixId = matrixId;
            _cellCount = cellCount;
            _xColumnNames = xColumnNames;
            _yColumnNames = yColumnNames;
            _context = context;
        }

        public void Upload()
        {
            for (var i = 0; i < _cellCount; i++)
            {
                var abscissaRecord = new Abscissa
                {
                    Name = _xColumnNames[i],
                };
                _context.Abscissa.Add(abscissaRecord);
                _context.SaveChanges();

                var ordinateRecord = new Ordinate
                {
                    Name = _yColumnNames[i],
                };
                _context.Ordinates.Add(ordinateRecord);
                _context.SaveChanges();

                var curveDataRecord = new CurveData
                {
                    FileId = _matrixId[i],
                    AbscissaId = abscissaRecord.AbscissaId,
                    OrdinateId = ordinateRecord.OrdinateId,
                };
                _context.CurveData.Add(curveDataRecord);
                _context.SaveChanges();
            }
        }
    }
}
